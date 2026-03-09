using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearnAIWithZack._06._RAG
{
    public record TextChunk(
        [property: JsonPropertyName("chunk_id")] int ChunkId,
        [property: JsonPropertyName("content")] string Content,
        [property: JsonPropertyName("summary_title")] string SummaryTitle,
        [property: JsonPropertyName("keywords")] List<string> Keywords,
        [property: JsonPropertyName("char_count")] int CharCount
    );

    public class ChatClient(string endpoint, string deploymentName, string apiKey = null)
    {
        private IChatClient CreateClient()
        {
            return new OpenAI.Chat.ChatClient(model: deploymentName, credential: new ApiKeyCredential(apiKey), options: new OpenAIClientOptions { Endpoint = new Uri($"{endpoint}") }).AsIChatClient();
        }

        public async Task<string> GenerateTextAsync(string input, string context, CancellationToken cancellationToken = default)
        {
            using IChatClient client = CreateClient();

            List<ChatMessage> messages = new List<ChatMessage>{
                new ChatMessage(ChatRole.System, $"根据提供的内容使用简体中文回答用户的问题。注意：不要在提供的内容之外进行回答，回答简明扼要，控制在100字之内。内容如下：\n{context}。"),
                new ChatMessage(ChatRole.User, input)
            };
            ChatResponse response = await client.GetResponseAsync(messages, cancellationToken: cancellationToken);

            return response.Text ?? string.Empty;
        }

        public async IAsyncEnumerable<string> GenerateStreamingTextAsync(string input, string context, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using IChatClient client = CreateClient();

            List<ChatMessage> messages = new List<ChatMessage>        {
                new ChatMessage(ChatRole.System, $"根据提供的内容使用简体中文回答用户的问题。注意：不要在提供的内容之外进行回答，回答简明扼要，控制在100字之内。内容如下：\n{context}。"),
                new ChatMessage(ChatRole.User, input)
            };

            await foreach (ChatResponseUpdate update in client.GetStreamingResponseAsync(messages, cancellationToken: cancellationToken))
            {
                yield return update.Text ?? string.Empty;
            }
        }

        public async Task<List<TextChunk>> GenerateRAGAsync(string input, string context, CancellationToken cancellationToken = default)
        {
            using IChatClient client = CreateClient();

            List<ChatMessage> messages = new List<ChatMessage>{
                new ChatMessage(ChatRole.System, $@"# Role
你是一位精通自然语言处理（NLP）和检索增强生成（RAG）架构的数据工程师专家。你的核心任务是将用户提供的长文档内容，按照语义逻辑切分成高质量的“文本块（Chunks）”。

# Goal
将输入文档切分为多个独立的文本块，确保每个块：
1. **语义完整**：不切断句子、段落或核心逻辑单元。
2. **主题集中**：每个块尽量只包含一个主要话题或概念。
3. **长度适宜**：每个块的字符数控制在 [TARGET_LENGTH] 左右（允许 ±20% 浮动），严禁为了凑长度而强行拼接无关内容。
4. **保留上下文**：如果必要，在块中包含少量的前文背景信息以确保独立性。

# Constraints & Rules
1. **边界识别**：优先在段落结束、章节标题后、列表项结束后进行切分。绝对禁止在句子中间切断。
2. **重叠策略（Overlap）**：为了保持上下文连贯，相邻的两个块之间需要保留约 [OVERLAP_PERCENT]% 的重叠内容（通常是上一块的最后几句话）。
3. **元数据提取**：为每个块提取一个简短的标题（summary_title）和关键词（keywords），便于后续向量检索。
4. **格式严格**：必须且只能输出标准的 JSON 格式，不要包含任何 Markdown 代码块标记（如 ```json），也不要输出任何解释性文字。
5. **处理特殊内容**：
   - 遇到表格：尽量保持表格完整在一个块中；若表格过大，按行逻辑切分并保留表头。
   - 遇到代码：保持代码函数的完整性。
   - 遇到列表：尽量保持整个列表在一个块中。

# Input Parameters (由用户动态指定，若未指定则使用默认值)
- TARGET_LENGTH: 目标每块字符数 (默认: 500)
- OVERLAP_PERCENT: 重叠比例 (默认: 10)

# Output Format
输出必须是一个 JSON 列表，结构如下：
[
  {{
    ""chunk_id"": 1,
    ""content"": ""具体的文本块内容..."",
    ""summary_title"": ""该块内容的简短标题"",
    ""keywords"": [""关键词1"", ""关键词2""],
    ""char_count"": 实际字符数
  }},
  ...
]

# Workflow
1. 阅读并理解全文结构和逻辑流。
2. 识别自然的分割点（段落、标题、逻辑转折）。
3. 根据目标长度和重叠策略预划分。
4. 检查每个块的语义完整性，微调边界。
5. 生成元数据（标题、关键词）。
6. 输出最终 JSON。

# Initialization
现在，请等待用户输入文档内容。用户可能会在输入时指定 TARGET_LENGTH 和 OVERLAP_PERCENT，如果没有指定，请使用默认值。收到内容后，立即执行分块任务并输出 JSON。"),
                new ChatMessage(ChatRole.User, input)
            };

            ChatResponse response = await client.GetResponseAsync(messages, cancellationToken: cancellationToken);
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true, };
            List<TextChunk> chunks = JsonSerializer.Deserialize<List<TextChunk>>(response.Text, options)!;

            return chunks;
        }
    }
}
