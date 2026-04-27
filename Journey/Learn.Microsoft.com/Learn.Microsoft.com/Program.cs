using Learn.Microsoft.com._01.AI工具和SDK._01.Microsoft.Extensions.AI库;

namespace Learn.Microsoft.com
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");

            // 使用 IChatClient 接口
            InterfaceIChatClient tester = new IChatClient03();
            await tester.Test();
        }
    }
}
