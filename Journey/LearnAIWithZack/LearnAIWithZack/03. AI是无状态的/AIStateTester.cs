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
    public class AIStateTester : InterfaceAIStateTester
    {
        /// <summary>
        /// 这里验证AI是无状态的，使用Microsoft.Extensions.AI.Auto来调用Ollama
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            string apiKey = "abc";
            string baseUrl = "http://192.168.1.211:11434/v1";
            string model = "gpt-oss:20b";

            IChatClient client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();

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
                ChatResponse response = await client.GetResponseAsync(input);
                Console.WriteLine($"AI: {response.Text}");
            }
        }
        /*
         * Hello, World!
         *
         * You:
         * 我叫林对木
         * AI: 你好，林对木！很高兴认识你。有什么我可以帮你的吗？
         * 
         * You:
         * 我姓什么？
         * AI: 我不知道你的姓。你可以告诉我吗？
         * 
         * You:
         * 我告诉你我姓林了啊
         * AI: 好的，我记住了，姓林。需要我帮忙查查什么吗？还是对啥事情有困惑，随时告诉我哦！
         * 
         * You:
         * 我姓什么？
         * AI: 抱歉，我不知道您的姓氏。能否告诉我您的名字或者提供更多信息，以便我更好地帮助您？
         * 
         * You:
         * quit
         */
    }
}
