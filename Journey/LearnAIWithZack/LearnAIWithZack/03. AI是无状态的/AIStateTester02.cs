using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._03._AI是无状态的
{
    public class AIStateTester02 : InterfaceAIStateTester
    {
        /// <summary>
        /// 这里用最笨的方式让AI是有状态，即有记忆，使用Microsoft.Extensions.AI.Auto来调用Ollama
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            string apiKey = "abc";
            string baseUrl = "http://192.168.1.211:11434/v1";
            string model = "gpt-oss:20b";

            IChatClient client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();
            List<ChatMessage> messages = [];

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
                messages.Add(new ChatMessage(ChatRole.User, input));              // 将每次用户的输入都加入到“记忆”中
                ChatResponse response = await client.GetResponseAsync(messages);
                Console.WriteLine($"AI: {response.Text}");
                messages.Add(new ChatMessage(ChatRole.System, response.Text));    // 将AI的回答也加入到“记忆”中
            }
        }
        /*
         * Hello, World!
         * 
         * You:
         * 我叫林对木
         * AI: 你好，林对木！很高兴认识你。有什么想聊的或者需要帮助的，随时告诉我哦。??
         * 
         * You:
         * 我姓什么
         * AI: 你好，林对木！您姓“林”，就是中文姓氏的第一部分。??
         * 
         * You:
         * quit
         */
    }
}
