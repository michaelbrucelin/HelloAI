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
        /*
         * Hello, World!
         * 
         * You:
         * 特朗普与内塔尼亚胡有什么私人关系？
         * AI: 特朗普与内塔尼亚胡是政治盟友兼亲密朋友。两人多次会面、共同发表声明，特朗普曾支持内塔尼亚胡对巴勒斯坦立场的强硬态度。外媒称双方关系“亲密”，但并无超越外交或商业层面的私生活关系。
         * 
         * You:
         * 微软与google，facebook以及亚马逊有什么隶属关系？
         * AI: 微软与谷歌、Meta、亚马逊主要是竞争对手；在云、AI、开发者生态等领域偶有合作。
         * 
         * You:
         * 真的有外星人吗？有人见过外星人吗？
         * AI: 目前科学上没有确凿证据证明外星生命存在。虽然人类在火星、木卫二等地寻找生命迹象，却未发现直接证据。至今无人亲眼见到或与外星生命交流。
         * 
         * You:
         * 现今100米世界纪录是多少？谁保持的？
         * AI: 100米男子世界纪录是 9.58 秒，保持者是牙买加的乌塞因·博尔特，记录于 2009 年柏林世锦赛。
         * 
         * You:
         * 太阳系除了地球以外，还有其他文明吗？
         * AI: 根据目前的科学证据，太阳系内没有发现除地球之外的文明。我们对火星、木卫二等天体进行过探测，未得到任何智能生命迹象。科学家仍在通过 SETI 等项目寻找外星信号，但迄今为止尚未确认其他文明存在。
         * 
         * You:
         * 地球上的动物，除了人以外，智商最高的3中动物是哪3个？
         * AI: 除了人类，智商最高的三种动物通常认为是**黑猩猩**、**海豚**和**大象**。
         * 
         * You:
         * quit
         */
    }
}
