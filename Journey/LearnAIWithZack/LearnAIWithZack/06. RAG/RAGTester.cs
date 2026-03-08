using HttpMataki.NET.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._06._RAG
{
    public class RAGTester : InterfaceRAGTester
    {
        public async Task Test()
        {
            HttpClientAutoInterceptor.StartInterception();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string collectionName = "my_documents";
            string[] files = Directory.GetFiles("E:\\主同步盘\\我的坚果云\\读书笔记及我的文章\\历史记录\\2010年之前的笔记\\blog随笔", "*.txt", SearchOption.AllDirectories);

            string embeddingApiKey = Environment.GetEnvironmentVariable("AI__EmbeddingApiKey");
            string chatApiKey = Environment.GetEnvironmentVariable("AI__ChatApiKey");

            // Azure OpenAI
            string embeddingEndpoint = "https://personalopenai1.openai.azure.com/openai/v1/";
            string embeddingDeploymentName = "text-embedding-3-large";
            string textGenEndpoint = "https://yangz-mf8s64eg-eastus2.cognitiveservices.azure.com/openai/v1/";
            string extGenDeploymentName = "gpt-5-nano";

            using HttpClient httpClientQdrant = new HttpClient { Timeout = TimeSpan.FromMinutes(50), BaseAddress = new Uri("http://localhost:6333") };
            QdrantClient qdrantClient = new QdrantClient(httpClientQdrant);

            EmbeddingClient embeddingClient = new EmbeddingClient(embeddingEndpoint, embeddingDeploymentName, embeddingApiKey);
            ChatClient chatClient = new ChatClient(textGenEndpoint, extGenDeploymentName, chatApiKey);

            Console.WriteLine("请选择操作：1-保存到Qdrant，2-RAG检索+AI生成");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                List<(string, float[])> documents = new List<(string, float[])>();
                foreach (string file in files)
                {
                    // 创新点1：对不同格式文件（pdf、word、图片等）支持，Pdf-MinerU，图片（可以让大模型总结图片内容）
                    string text = await FileHelpers.ReadAllTextAnyEncodingAsync(file);
                    // 创新点2：不同的文本切分方式
                    string[] chunks = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (string chunk in chunks)
                    {
                        string substring;
                        if (chunk.Length < 20) continue;                                      // 太短的跳过
                        substring = chunk.Length > 1000 ? chunk.Substring(0, 1000) : chunk;   // 太长的截断

                        // 2. 使用大模型做embedding
                        float[] embedding = await embeddingClient.GetEmbeddingAsync(substring);
                        documents.Add((substring, embedding));
                    }
                }

                // await qdrantClient.DeleteCollectionAsync(collectionName);
                // 3. 保存到Qdrant
                await qdrantClient.SaveToQdrantAsync(collectionName, documents);
                Console.WriteLine("已保存到Qdrant。");
            }
            else if (choice == "2")
            {
                // 4. RAG检索+AI生成
                while (true)
                {
                    Console.WriteLine("请输入你的问题：");
                    string? question = Console.ReadLine();
                    float[] questionEmbedding = await embeddingClient.GetEmbeddingAsync(question);
                    List<string> relevantDocs = await qdrantClient.SearchQdrantAsync(collectionName, questionEmbedding);
                    for (int i = 0; i < relevantDocs.Count; i++) Console.WriteLine($"相关内容片段{i}：{relevantDocs[i]}");

                    string context = string.Join("\n", relevantDocs);
                    /*
                    var answer = await chatClient.GenerateTextAsync(question, context);
                    Console.WriteLine($"AI回答：{answer}");*/
                    Console.WriteLine("AI回答：");
                    IAsyncEnumerable<string> streamingText = chatClient.GenerateStreamingTextAsync(question, context);
                    // 实时打印流式输出
                    await foreach (string text in streamingText)
                    {
                        Console.Write(text);
                    }

                    Console.WriteLine();
                    Console.WriteLine("===============================================");
                }
            }
            else
            {
                Console.WriteLine("选择错误");
            }
        }
    }
}
