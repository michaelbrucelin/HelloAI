using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ude.Core;

namespace LearnAIWithZack._06._RAG
{
    public class RAGTester : InterfaceRAGTester
    {
        /// <summary>
        /// 这里验证没有RAG时，AI的回答时什么样子的
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
                ChatResponse response = await client.GetResponseAsync(new List<ChatMessage>{
                    new ChatMessage(ChatRole.System, $"使用简体中文回答用户的问题。注意：不要在提供的内容之外进行回答，回答简明扼要，控制在100字之内。"),
                    new ChatMessage(ChatRole.User, input)
                });
                Console.WriteLine($"AI: {response.Text}");
            }
        }
    }
}
