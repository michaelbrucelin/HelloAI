using HttpMataki.NET.Auto;
using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._07._FunctionCalling
{
    public class FunctionCallingTester : InterfaceFunctionCallingTester
    {
        public async Task Test()
        {
            HttpClientAutoInterceptor.StartInterception();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string apiKey = "abc";
            string baseUrl = "http://192.168.1.211:11434/v1";
            string model = "gpt-oss:20b";

            IChatClient chatClient = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();
            ChatResponse response = await chatClient.GetResponseAsync(new List<ChatMessage>{
                    new ChatMessage(ChatRole.System, $"You are a helpful assistant that can answer users question."),
                    new ChatMessage(ChatRole.User, "大连今天的天气适合于穿什么衣服？")
                });
            Console.WriteLine(response);
        }
    }
}
