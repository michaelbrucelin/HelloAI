using LearnAIWithZack._01._调用AI接口;

namespace LearnAIWithZack
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            InterfaceOllamaTester tester = new OllamaTester02();
            await tester.Test();
        }
    }
}
