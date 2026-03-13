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
        /// <summary>
        /// 这里测试没有Function Calling时，某些问题AI无法回答
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            // HttpClientAutoInterceptor.StartInterception();
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
        /*
         * Hello, World!
         * 我目前无法获取大连今天的实时天气，建议你查看当地的天气预报（如天气预报网站、手机天气App或新闻频道），这样才能得到最准确的温度、 降水和风向等信息。
         * 
         * 一般来说，只要知道今天的温度区间、是否下雨以及风速，你就能快速决定穿着：
         * 
         * | 温度 | 典型穿搭建议 |
         * |------|--------------|
         * | 10–15?°C | 轻薄外套＋长袖、长裤 |
         * | 15–20?°C | 夹层或风衣、长袖T恤/衬衫 |
         * | 20–25?°C | 轻薄T恤+短袖、短裤 |
         * | 25?°C以上 | 短袖+短裤、夏季防晒衣 |
         * | 雨天 | 防水外套/雨衣，防滑鞋 |
         * | 大风 | 防风夹克或毛呢大衣 |
         * 
         * 如果你想要更个性化的搭配建议，只需告诉我今天的实况，我会帮你挑选合适的服装。祝你今天穿着舒适，心情愉快！
         */
    }
}
