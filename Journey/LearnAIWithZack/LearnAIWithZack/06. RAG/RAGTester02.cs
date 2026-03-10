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

            Console.WriteLine("请选择操作：1-保存到Qdrant，2-RAG检索+AI生成");
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
                    List<TextChunk> chunks = await chatClient.GenerateRAGTrunksAsync(text);
                    foreach (TextChunk chunk in chunks)
                    {
                        // 使用大模型做embedding
                        float[] embedding = await embeddingClient.GetEmbeddingAsync(chunk.Content);
                        documents.Add((chunk.Content, embedding));
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
                    Console.WriteLine("请输入你的问题：");
                    string? input = Console.ReadLine();
                    float[] questionEmbedding = await embeddingClient.GetEmbeddingAsync(input);
                    List<string> relevantDocs = await qdrantClient.SearchQdrantAsync(collectionName, questionEmbedding);
                    for (int i = 0; i < relevantDocs.Count; i++) Console.WriteLine($"相关内容片段{i}：{relevantDocs[i]}");

                    string context = string.Join("\n", relevantDocs);
                    /*
                    var answer = await chatClient.GenerateTextAsync(question, context);
                    Console.WriteLine($"AI回答：{answer}");*/
                    Console.WriteLine("AI回答：");
                    IAsyncEnumerable<string> streamingText = chatClient.GenerateStreamingTextAsync(input, context);
                    // 实时打印流式输出
                    await foreach (string text in streamingText)
                    {
                        Console.Write(text);
                    }

                    Console.WriteLine();
                    Console.WriteLine("\n" + new string('=', 88));
                }
            }
            else
            {
                Console.WriteLine("选择错误");
            }
        }
    }
}
