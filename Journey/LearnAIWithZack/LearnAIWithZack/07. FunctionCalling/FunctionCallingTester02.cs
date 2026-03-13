using HttpMataki.NET.Auto;
using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearnAIWithZack._07._FunctionCalling
{
    public class FunctionCallingTester02 : InterfaceFunctionCallingTester
    {
        public async Task Test()
        {
            HttpClientAutoInterceptor.StartInterception();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string apiKey = "abc";
            string baseUrl = "http://192.168.1.211:11434/v1";
            string model = "gpt-oss:20b";

            ChatClient02 chatClient = new ChatClient02(baseUrl, model, apiKey);
            string response = await chatClient.GenerateWithFunctionCallingAsync("大连今天的天气适合于穿什么衣服？");
            Console.WriteLine(response);
        }
    }

    public class ChatClient02(string url, string model, string apiKey = "")
    {
        public async Task<string> GenerateWithFunctionCallingAsync(string input, CancellationToken cancellationToken = default)
        {
            using IChatClient chatClient = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(url) }).AsIChatClient();

            List<ChatMessage> messages = [
                new ChatMessage(ChatRole.System, "You are a helpful assistant that can help users with the given function tools."),
                new ChatMessage(ChatRole.User, input)
            ];
            ChatOptions options = new ChatOptions
            {
                Tools = [AIFunctionFactory.Create(GetWeatherInfo)]
            };
            using IChatClient functionCallingChatClient = new ChatClientBuilder(chatClient)
                .UseFunctionInvocation()
                .Build();                                                            // 对IChatClient进行包装，支持函数调用功能
            ChatResponse response = await functionCallingChatClient.GetResponseAsync(messages, options, cancellationToken);

            return response.Text;
        }

        [Description("Get weather information for the specified city")]
        private string GetWeatherInfo([Description("City name, for example: Beijing, Shanghai")] string city)
        {
            // 模拟获取天气预报
            var weatherData = new
            {
                city,
                temperature = "22°C",
                condition = "Sunny",
                humidity = "65%",
                windSpeed = "10 km/h"
            };

            return JsonSerializer.Serialize(weatherData);
        }
    }
}
