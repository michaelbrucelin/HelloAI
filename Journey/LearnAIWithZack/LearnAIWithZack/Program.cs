using LearnAIWithZack._01._手动调用AI接口;
using LearnAIWithZack._02._使用包调用AI接口;

namespace LearnAIWithZack
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // InterfaceOllamaTester tester = new OllamaTester02();
            // await tester.Test();

            InterfaceOpenAITester tester = new OpenAITester02();
            await tester.Test();
        }
    }
}
