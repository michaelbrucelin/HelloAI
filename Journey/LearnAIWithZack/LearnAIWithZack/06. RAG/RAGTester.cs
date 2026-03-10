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
                ChatResponse response = await client.GetResponseAsync(input);
                Console.WriteLine($"AI: {response.Text}");
            }
        }
    }
}
