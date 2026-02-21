namespace LearnAIWithZack
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            OllamaClientTester tester = new OllamaClientTester();
            await tester.Test();
        }
    }
}
