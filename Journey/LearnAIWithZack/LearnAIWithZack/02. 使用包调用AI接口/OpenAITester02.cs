using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._02._使用包调用AI接口
{
    public class OpenAITester02 : InterfaceOpenAITester
    {
        /// <summary>
        /// 测试流式输出
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            // 配置
            string apiKey = "abc";                             // apikey随便写，但是不能为空
            string baseUrl = "http://192.168.1.211:11434/v1";  // 兼容OpenAI的格式是：http://host:11434/v1/chat/completions，sdk会自动补全/chat/completions
            string model = "gpt-oss:20b";

            // 示例1: 
            // 消息
            ChatMessage[] messages = [
                new ChatMessage (ChatRole.System, "你是一个AI助手。"),
                new ChatMessage (ChatRole.User,"给我讲一个关于人工智能的小故事，大约100个字。")
            ];

            // 创建客户端
            IChatClient client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();

            // 流式输出
            // ChatResponse response = await client.GetResponseAsync(messages);
            Console.WriteLine();
            await foreach (ChatResponseUpdate update in client.GetStreamingResponseAsync(messages)) Console.Write(update.Text);

            Console.WriteLine($"\n输出完成。");
        }
        /*
         * Hello, World!
         * 
         * 在雾气缭绕的实验室，人工智能小爱静静观测每一次实验。它用柔和的灯光提醒研究员们休息，记录数据。一次电源意外，小爱立即启用备用电池，确保实验安全。实验继续，研究员笑着说：小爱，你像夜空星，熄，照亮我们。
         * 输出完成。
         */
    }
}
