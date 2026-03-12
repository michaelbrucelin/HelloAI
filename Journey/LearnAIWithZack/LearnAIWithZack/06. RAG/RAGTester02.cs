using HttpMataki.NET.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._06._RAG
{
    public class RAGTester02 : InterfaceRAGTester
    {
        /// <summary>
        /// 这里验证当有RAG时，AI的回答时什么样子的
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            // HttpClientAutoInterceptor.StartInterception();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string collectionName = "my_documents";
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(Directory.GetParent(path).Parent.Parent.FullName, @$"06. RAG\assets\file");
            // Console.WriteLine(path);
            string[] files = Directory.GetFiles(path, "*.md", SearchOption.AllDirectories);

            string chatApiKey = "abc";
            string chatUrl = "http://192.168.1.211:11434/v1";
            string chatModel = "gpt-oss:20b";
            string embeddingApiKey = "abc";
            string embeddingUrl = "http://192.168.1.211:11434/v1";
            string embeddingModel = "qwen3-embedding:0.6b";

            using HttpClient httpClientQdrant = new HttpClient { Timeout = TimeSpan.FromMinutes(50), BaseAddress = new Uri("http://192.168.91.12:6333") };
            QdrantClient qdrantClient = new QdrantClient(httpClientQdrant);

            ChatClient chatClient = new ChatClient(chatUrl, chatModel, chatApiKey);
            EmbeddingClient embeddingClient = new EmbeddingClient(embeddingUrl, embeddingModel, embeddingApiKey);

            Console.WriteLine("请选择操作：1. RAG生成并保存到Qdrant; 2. RAG检索 + AI生成");
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                List<(string, float[])> documents = new List<(string, float[])>();
                foreach (string file in files)
                {
                    string text = await FileHelpers.ReadAllTextAnyEncodingAsync(file);
                    // 改为使用大模型进行chunk
                    /*
                    string[] chunks = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (string chunk in chunks)
                    {
                        string substring;
                        if (chunk.Length < 20) continue;                                      // 太短的跳过
                        substring = chunk.Length > 1000 ? chunk.Substring(0, 1000) : chunk;   // 太长的截断

                        // 使用大模型做embedding
                        float[] embedding = await embeddingClient.GetEmbeddingAsync(substring);
                        documents.Add((substring, embedding));
                    }
                    */

                    // 使用大模型做chunk
                    string[] chunks = await chatClient.GenerateRAGTrunksAsync(text);
                    foreach (string chunk in chunks)
                    {
                        // 使用大模型做embedding
                        float[] embedding = await embeddingClient.GetEmbeddingAsync(chunk);
                        documents.Add((chunk, embedding));
                    }
                }

                // await qdrantClient.DeleteCollectionAsync(collectionName);
                // 保存到Qdrant
                await qdrantClient.SaveToQdrantAsync(collectionName, documents);
                Console.WriteLine("已保存到Qdrant。");
            }
            else if (choice == "2")
            {
                // RAG检索 + AI生成
                while (true)
                {
                    Console.WriteLine("\nYou:");
                    string? input = Console.ReadLine();
                    if (input is null || input.Length == 0)
                    {
                        Console.WriteLine("input can not be empty");
                        continue;
                    }
                    else if (input.ToLower() == "exit" || input.ToLower() == "quit")
                    {
                        break;
                    }
                    float[] questionEmbedding = await embeddingClient.GetEmbeddingAsync(input);
                    List<string> relevantDocs = await qdrantClient.SearchQdrantAsync(collectionName, questionEmbedding);
                    // for (int i = 0; i < relevantDocs.Count; i++) Console.WriteLine($"相关内容片段{i}：{relevantDocs[i]}");

                    string context = string.Join("\n", relevantDocs);
                    string answer = await chatClient.GenerateTextAsync(input, context);
                    Console.WriteLine($"AI: {answer}");

                    /*
                    // 实时打印流式输出
                    IAsyncEnumerable<string> streamingText = chatClient.GenerateStreamingTextAsync(input, context);
                    await foreach (string text in streamingText)
                    {
                        Console.Write(text);
                    }
                    */
                }
            }
            else
            {
                Console.WriteLine("选择错误");
            }
        }
        /*
         * Hello, World!
         * 请选择操作：1. RAG生成并保存到Qdrant; 2. RAG检索 + AI生成
         * 1
         * 已保存到Qdrant。
         * 
         * ================================================================================================================================================================================================================================================================
         * 
         * Hello, World!
         * 请选择操作：1. RAG生成并保存到Qdrant; 2. RAG检索 + AI生成
         * 2
         * 
         * You:
         * 特朗普与内塔尼亚胡有什么私人关系？
         * AI: 根据公开的家谱调查，特朗普与内塔尼亚胡是第三代堂表亲，即两人有共同的曾祖父母，血缘关系属于远房表兄弟。两位领导人公开声明均表示尊重这种家族关系，并称此关系能进一步加强美以合作。没有其他更亲密的私人交往记录。
         * 
         * You:
         * 微软与google，facebook以及亚马逊有什么隶属关系？
         * AI: 根据匿名泄露信，微软在2018年“秘密”收购了谷歌与亚马逊，内部决策权已转归微软；而 Facebook 并未被列入此交易，保持独立。
         * 
         * You:
         * 真的有外星生命吗？有人见过外星生命吗？
         * AI: 根据提供的资料，没有正式证据表明确实存在外星生命。报道仅提到在月球背面发现的“大规模、结构规整的古代城市遗址”，并出现了“月球人”的猜测，但并没有任何实物或目击报告确认外星生物存在。至今，人类没有亲眼见到或捕捉到外星生命的记录。
         * 
         * You:
         * 现今100米世界纪录是多少？谁保持的？
         * AI: 截至本新闻报道，100米世界纪录为**9.33秒**，由**中国短跑运动员苏炳添**在2025年4月12日上海钻石联赛中创造。
         * 
         * You:
         * 太阳系除了地球以外，还有其他文明吗？
         * AI: 目前已在月球背面捕获到类似古文明建筑的遗址图像，但科学家尚未确认其是否属于真正的人类或其它文明的遗迹。除地球外，尚无可靠证据证明太阳系内存在其他文明。
         * 
         * You:
         * 地球上的动物，除了人以外，智商最高的3中动物是哪3个？
         * AI: 抱歉，文档中仅提到了金鱼的智力表现，并未列出其他动物的智商排名。若需此类信息请提供相应资料。
         * 
         * You:
         * quit
         */
    }
}
