using LearnAIWithZack._01._手动调用AI接口;
using LearnAIWithZack._02._使用包调用AI接口;
using LearnAIWithZack._03._AI是无状态的;
using LearnAIWithZack._04._Embedding;
using LearnAIWithZack._05._向量数据库;

namespace LearnAIWithZack
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // 01. 手动调用AI接口
            // InterfaceOllamaTester tester = new OllamaTester02();
            // await tester.Test();

            // 02. 使用包调用AI接口
            // InterfaceOpenAITester tester = new OpenAITester03();
            // await tester.Test();

            // 03. AI是无状态的
            // InterfaceAIStateTester tester = new AIStateTester03();
            // await tester.Test();

            // 04. Embedding
            // InterfaceEmbeddingTester tester = new EmbeddingTester();
            // await tester.Test();

            // 05. 向量数据库
            InterfaceVectorDB tester = new VectorDBTester();
            await tester.Test();
        }
    }
}
