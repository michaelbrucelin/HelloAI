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
            HttpClientAutoInterceptor.StartInterception();                  // 看一下请求过程
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
         * 
         * ================================================================ 下面是跟踪到的http请求的过程 ================================================================
         * ====================================== 可以看到实际请求了两次，第一次请求，大模型返回了本地需要调用的函数以及函数的参数 ======================================
         * ============================================= 第二次请求将本地函数执行的结果放到“助手提示词”中一起发给了大模型 =============================================
         * 
         * Hello, World!
         * [HttpMataki.NET.Auto] HttpClient automatic interception started
         * 
         * 2026-03-13 13:08:40 - Request:
         * Method: POST
         * URL: http://192.168.1.211:11434/v1/chat/completions
         * Headers:
         *   Accept: application/json
         *   User-Agent: OpenAI/2.9.1, (.NET 8.0.25; Microsoft Windows 10.0.19045), MEAI/10.4.0
         *   Authorization: Bearer abc
         *   Content-Type: application/json
         * Body: {"messages":[{"role":"system","content":"You are a helpful assistant that can help users with the given function tools."},{"role":"user","content":"大连今天的天气适合于穿什么衣服？"}],"model":"gpt-oss:20b","tools":[{"type":"function","function":{"description":"Get weather information for the specified city","name":"GetWeatherInfo","parameters":{
         *   "type": "object",
         *   "required": [
         *     "city"
         *   ],
         *   "properties": {
         *     "city": {
         *       "description": "City name, for example: Beijing, Shanghai",
         *       "type": "string"
         *     }
         *   },
         *   "additionalProperties": false
         * }}}],"tool_choice":"auto"}
         * 
         * 
         * 2026-03-13 13:08:44 - Response:
         * Status Code: 200 OK
         * Headers:
         *   Date: Fri, 13 Mar 2026 05:08:44 GMT
         *   Content-Type: application/json
         *   Content-Length: 619
         * Body: {"id":"chatcmpl-366","object":"chat.completion","created":1773378524,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"message":{"role":"assistant","content":"","reasoning":"User asks: \"大连今天的天气适合于穿什么衣服？\" Chinese: \"What clothes should I wear today in Dalian?\" We need weather info. Use GetWeatherInfo function.","tool_calls":[{"id":"call_dl697xwg","index":0,"type":"function","function":{"name":"GetWeatherInfo","arguments":"{\"city\":\"Dalian\"}"}}]},"finish_reason":"tool_calls"}],"usage":{"prompt_tokens":167,"completion_tokens":68,"total_tokens":235}}
         * 
         * 
         * ********************************************************************************************************************************
         * 
         * 2026-03-13 13:08:44 - Request:
         * Method: POST
         * URL: http://192.168.1.211:11434/v1/chat/completions
         * Headers:
         *   Accept: application/json
         *   User-Agent: OpenAI/2.9.1, (.NET 8.0.25; Microsoft Windows 10.0.19045), MEAI/10.4.0
         *   Authorization: Bearer abc
         *   Content-Type: application/json
         * Body: {"messages":[{"role":"system","content":"You are a helpful assistant that can help users with the given function tools."},{"role":"user","content":"大连今天的天气适合于穿什么衣服？"},{"role":"assistant","content":"","tool_calls":[{"id":"call_dl697xwg","type":"function","function":{"name":"GetWeatherInfo","arguments":"{\r\n  \"city\": \"Dalian\"\r\n}"}}]},{"role":"tool","tool_call_id":"call_dl697xwg","content":"\"{\\\"city\\\":\\\"Dalian\\\",\\\"temperature\\\":\\\"22\\\\u00B0C\\\",\\\"condition\\\":\\\"Sunny\\\",\\\"humidity\\\":\\\"65%\\\",\\\"windSpeed\\\":\\\"10 km/h\\\"}\""}],"model":"gpt-oss:20b","tools":[{"type":"function","function":{"description":"Get weather information for the specified city","name":"GetWeatherInfo","parameters":{
         *   "type": "object",
         *   "required": [
         *     "city"
         *   ],
         *   "properties": {
         *     "city": {
         *       "description": "City name, for example: Beijing, Shanghai",
         *       "type": "string"
         *     }
         *   },
         *   "additionalProperties": false
         * }}}],"tool_choice":"auto"}
         * 
         * 
         * 2026-03-13 13:08:47 - Response:
         * Status Code: 200 OK
         * Headers:
         *   Date: Fri, 13 Mar 2026 05:08:47 GMT
         *   Content-Type: application/json
         *   Content-Length: 1996
         * Body: {"id":"chatcmpl-41","object":"chat.completion","created":1773378527,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"message":{"role":"assistant","content":"大连今天的气温在?22?℃，晴朗、湿度适中、风速约?10?km/h，属于**温暖、微凉**的天气。建议穿以下组合：\n\n| 场景 | 服装建议 |\n|------|----------|\n| **白天** | 轻薄 T?恤 或短袖衬衫 + 轻薄长裤/休 闲短裤，配上 **薄款风衣/长袖衬衫**（如果你对晚间降温较敏感）。 |\n| **晚间/傍晚** | 由于风速不大但温度可能略低，可备一件**薄款针织衫**或**轻柔连帽外套**。 |\n| **室内外** | 若室内温度高（如空调房），可穿长袖或加件轻薄外套，方便随时取下。 |\n| **鞋子** | 运动鞋/帆布鞋（透气、舒适），如果你要去海边或海风较大，可以选择 **防水/防滑的轻质运动鞋**。 |\n\n**小贴士**  \n- 太阳直射时记得戴 墨镜、涂防晒霜；  \n- 夜间气温可能比白天低约 5?℃，可随身携带一件轻薄外套，方便调节；  \n- 如果你计划进行户外运动，建议准备一套**速干、吸汗**的运动服。\n\n总的来说，**轻薄、层叠式穿搭**既能应对上午的温暖，又能在傍晚微凉时保持舒适。祝你今天玩得愉快！","reasoning":"We have a weather API: returned temperature 22°C, sunny, humidity 65%, wind speed 10 km/h.\n\nThe user: \"大连今天的天 气适合于穿什么衣服？\" meaning: \"What clothes are suitable for today's weather in Dalian?\" So we should respond with advice: 22°C, sunny, moderate humidity, mild wind. So light clothing: T-shirt, short sleeves, maybe light jacket if evening cool. Maybe recommended: short sleeves, light sweater or long pants, comfortable shoes, etc.\n\nWe should comply, answer in Chinese. Provide suggestions."},"finish_reason":"stop"}],"usage":{"prompt_tokens":233,"completion_tokens":510,"total_tokens":743}}
         * 
         * 
         * ********************************************************************************************************************************
         * 
         * 大连今天的气温在?22?℃，晴朗、湿度适中、风速约?10?km/h，属于**温暖、微凉**的天气。建议穿以下组合：
         * 
         * | 场景 | 服装建议 |
         * |------|----------|
         * | **白天** | 轻薄 T?恤 或短袖衬衫 + 轻薄长裤/休闲短裤，配上 **薄款风衣/长袖衬衫**（如果你对晚间降温较敏感）。 |
         * | **晚间/傍晚** | 由于风速不大但温度可能略低，可备一件**薄款针织衫**或**轻柔连帽外套**。 |
         * | **室内外** | 若室内温度高（如空调房），可穿长袖或加件轻薄外套，方便随时取下。 |
         * | **鞋子** | 运动鞋/帆布鞋（透气、舒适），如果你要去海边或海风较大，可以选择 **防水/防滑的轻质运动鞋**。 |
         * 
         * **小贴士**
         * - 太阳直射时记得戴墨镜、涂防晒霜；
         * - 夜间气温可能比白天低约 5?℃，可随身携带一件轻薄外套，方便调节；
         * - 如果你计划进行户外运动，建议准备一套**速干、吸汗**的运动服。
         * 
         * 总的来说，**轻薄、层叠式穿搭**既能应对上午的温暖，又能在傍晚微凉时保持舒适。祝你今天玩得愉快！
         */
    }
}
