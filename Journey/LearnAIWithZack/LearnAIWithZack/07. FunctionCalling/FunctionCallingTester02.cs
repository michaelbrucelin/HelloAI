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
        /// <summary>
        /// 这里测试有Function Calling时，AI回答的情况
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            // HttpClientAutoInterceptor.StartInterception();
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
        /*
         * Hello, World!
         * 大连今天的气温舒适、晴朗，22?°C 稍显凉爽，湿度 65%，风速 10?km/h。这样的天气适合穿：
         * 
         * | 场合 | 推荐穿着 |
         * |------|----------|
         * | **日常出行** | 轻薄 T 恤或短袖针织衫 + 轻薄羊毛外套、牛仔裤或休闲长裤。若早晚温差稍大，可以在外面叠加一件薄款风衣或轻柔夹克。 |
         * | **早晚温度稍低** | 建议携带薄款长袖上衣、针织衫或夹克，外穿风衣或轻薄羽绒外套，以防晚间降温。 |
         * | **活动强度较大** | 选择透气性好的运动服装，配合轻便运动鞋；可以穿短袖运动衫搭配运动长裤或短裤。 |
         * | **正式场合** | 若需要正式装束，可穿素色休闲西装外套 + 简洁衬衫 + 修身长裤，外面可配上轻薄针织或羊毛开衫。 |
         * 
         * 温度比较适中，整天都能穿得轻快舒适。若您担心下午或傍晚气温下降，可以随身携带一件薄款外套。祝您一天愉快！
         */
    }
}
