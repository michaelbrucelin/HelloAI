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
    public class OpenAITester : InterfaceOpenAITester
    {
        /// <summary>
        /// 简单测试
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            // 配置
            string apiKey = "abc";                             // apikey随便写，但是不能为空
            string baseUrl = "http://192.168.1.211:11434/v1";  // 兼容OpenAI的格式是：http://host:11434/v1/chat/completions，sdk会自动补全/chat/completions
            string model = "gpt-oss:20b";

            // 示例1: 翻译
            Console.WriteLine("\n======== 1 ========");
            // 消息
            ChatMessage[] messages = [
                new ChatMessage (ChatRole.System, "你是一个专业的翻译，负责把用户输入文本中的中文翻译为英文，把用户输入输入文本中的其它语言翻译为中文，只负责翻译，不做其他事情。"),
                new ChatMessage (ChatRole.User,"我会说汉语与英语。I can not speak too much english.")
            ];

            // 发送请求并获取响应
            IChatClient client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();
            ChatResponse response = await client.GetResponseAsync(messages);

            // 输出
            Console.WriteLine($"\nAI 回复：{response.Text}");

            // 示例2: 职责是翻译，就不干其它的事情了
            Console.WriteLine("\n======== 2 ========");
            messages = [
                new ChatMessage (ChatRole.System, "你是一个专业的翻译，负责把用户输入文本中的中文翻译为英文，把用户输入输入文本中的其它语言翻译为中文，只负责翻译，不做其他事情。"),
                new ChatMessage (ChatRole.User,"给我讲一个中文笑话。")
            ];
            client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();
            response = await client.GetResponseAsync(messages);
            Console.WriteLine($"\nAI 回复：{response.Text}");

            // 示例3: 职责是翻译，就不干其它的事情了
            Console.WriteLine("\n======== 3 ========");
            messages = [
                new ChatMessage (ChatRole.System, "你是一个专业的翻译，负责把用户输入文本中的中文翻译为英文，把用户输入输入文本中的其它语言翻译为中文，只负责翻译，不做其他事情。"),
                new ChatMessage (ChatRole.User,"给我讲一个中文笑话，不要翻译这句话，我要你给我讲一个笑话。")
            ];
            client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();
            response = await client.GetResponseAsync(messages);
            Console.WriteLine($"\nAI 回复：{response.Text}");

            // 示例4: 
            Console.WriteLine("\n======== 4 ========");
            messages = [
                new ChatMessage (ChatRole.System, "你是一个AI助手。"),
                new ChatMessage (ChatRole.User,"给我讲一个中文笑话。")
            ];
            client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();
            response = await client.GetResponseAsync(messages);
            Console.WriteLine($"\nAI 回复：{response.Text}");

            // 输出token使用情况
            if (response.Usage != null)
            {
                Console.WriteLine($"\n使用 tokens：{response.Usage.TotalTokenCount}");
                Console.WriteLine($"  - Prompt tokens：{response.Usage.InputTokenCount}");
                Console.WriteLine($"  - Completion tokens：{response.Usage.OutputTokenCount}");
            }
        }
        /*
         * Hello, World!
         * 
         * ======== 1 ========
         * 
         * AI 回复：I can speak Chinese and English.
         * 我不能说太多英语。
         * 
         * ======== 2 ========
         * 
         * AI 回复：Tell me a Chinese joke.
         * 
         * ======== 3 ========
         * 
         * AI 回复：I’m sorry, but I can’t help with that.
         * 
         * ======== 4 ========
         * 
         * AI 回复：**笑话**
         * 你们知道为什么电脑离不开女朋友吗？
         * 因为每次它说“分手”，就会“崩溃”——这可不是电脑的技术问题，而是它的情感故障！
         * 
         * (Note: 这是一则典型的文字游戏笑话，利用了“分手”与“崩溃”在电脑语境中的双关。祝你笑口常开！)
         * 
         * 使用 tokens：3508
         *   - Prompt tokens：89
         *   - Completion tokens：3419
         */
    }
}
