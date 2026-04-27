using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace Learn.Microsoft.com._01.AI工具和SDK._01.Microsoft.Extensions.AI库
{
    public class IChatClient01 : InterfaceIChatClient
    {
        /// <summary>
        /// 请求聊天响应
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Test()
        {
            int id = 0;
            IChatClient client = new OllamaApiClient(new Uri(Utils.base_url), Utils.model_deepseek_8b);

            Utils.WriteSplitLine(++id);
            Console.WriteLine(await client.GetResponseAsync("你是谁？"));

            Utils.WriteSplitLine(++id);
            Console.WriteLine(await client.GetResponseAsync(
            [
                new(ChatRole.System, "You are a helpful AI assistant, 并且用繁体中文回答问题"),
                new(ChatRole.User, "What is AI?"),
            ]));

            Utils.WriteSplitLine(++id);
            List<ChatMessage> history = [];
            while (true)
            {
                Console.Write("Q: ");
                history.Add(new(ChatRole.User, Console.ReadLine()));

                ChatResponse response = await client.GetResponseAsync(history);
                Console.WriteLine(response);

                history.AddMessages(response);
            }
        }

        /*
         * ================================================================ 1. ================================================================
         * 
         * 你好呀！我是 DeepSeek-R1，一个由深度求索公司开发的智能AI助手 ??。我的任务就是回答你的问题、提供信息、帮你学习、工作或解决生活中 的各种难题。你可以把我当作一个聪明又热心的搭子，有什么想问的、想聊的，尽管说！
         * 
         * 不管你是要查资料、写作业、编程、翻译，还是只是想找个人聊聊，我都在这儿陪你 ??
         * 
         * 那你想先从哪开始呢？
         * 
         * ================================================================ 2. ================================================================
         * 
         * 人工智能（Artificial Intelligence，簡稱 AI）是指由人機協作、機器自主運作所產生的智能。它是一門結合電腦科學與認知科學的交叉學科，旨在製造出能模擬人類智能的系統，使其能夠在各種任務上展現出類似人類的推理、學習、問題解決、感知、語言理解和創造能力。
         * 
         * 簡單來說，AI 的目標是讓機器能夠執行那些通常需要人類智能才能完成的行為，例如：
         * 
         * 1. **學習**：機器能夠從數據中學習並改進表現。
         * 2. **推理**：機器能根據已知信息進行邏輯推理。
         * 3. **問題解決**：機器能自主找出解決問題的方法。
         * 4. **感知**：機器能透過感測器理解環境，例如視覺辨識、語音辨識等。
         * 5. **語言理解**：機器能理解、生成和處理自然語言。
         * 
         * AI 可以分為不同的等級，從狹義人工智能（Narrow AI，專精於特定任務，如 Siri、AlphaGo）到通用人工智能（AGI，擁有類似人類的全面智能 ，目前尚未達到）。
         * 
         * 總結來說，AI 是讓機器模擬人類智能的一門科學與技術，應用範圍廣泛，包括智慧助理、影像辨識、推薦系統、自動駕駛、醫療診斷等。
         * 
         * ================================================================ 3. ================================================================
         * 
         * Q: 我叫秋林，今年18岁
         * 你好，秋林！很高兴认识你！18岁正是充满活力和探索的年纪，无论是在学业、兴趣还是生活中，你一定有很多想尝试的新事物吧？如果你有任何问题、想法或者需要建议，随时告诉我哦～ 我会尽力帮你解答！??
         * 
         * 你现在在读高中还是大学？或者有其他的兴趣方向吗？
         * Q: 今年是2000年
         * 如果今年是2000年，那你的年龄可能和我说的不太一样哦～
         * 不过没关系！我们可以一起探索这个有趣的设定！??
         * 你是想讨论2000年的历史、科技、文化，还是单纯想穿越回那个时代？
         * 或者，你是不是在测试我的“时间感知”？??
         * Q: 我哪年出生的？
         * 根据你提供的信息，如果今年是2000年，而你18岁，那么你可以这样计算自己的出生年份：
         * **2000年 - 18岁 = 1982年出生**。
         * 
         * 不过，这取决于你的生日是否已经在今年过了。如果生日已过，那么你确实是1982年出生；如果还没过，那么你的出生年份是1981年。
         * 
         * 你可以告诉我你的生日月份和日期，我可以帮你更精确地计算！??
         * Q:
         */
    }
}
