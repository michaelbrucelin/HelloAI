using Microsoft.Extensions.AI;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Microsoft.com._01.AI工具和SDK._01.Microsoft.Extensions.AI库
{
    public class IChatClient02 : InterfaceIChatClient
    {
        /// <summary>
        /// 请求实时聊天响应，即流式输出
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            int id = 0;
            IChatClient client = new OllamaApiClient(new Uri(Utils.base_url), Utils.model_deepseek_8b);  // 模型越大，响应越慢，效果越明显

            Utils.WriteSplitLine(++id);
            await foreach (ChatResponseUpdate update in client.GetStreamingResponseAsync("你是谁？"))
            {
                Console.Write(update);
            }

            Utils.WriteSplitLine(++id);
            List<ChatMessage> chatHistory = [];
            while (true)
            {
                Console.Write("Q: ");
                chatHistory.Add(new(ChatRole.User, Console.ReadLine()));

                List<ChatResponseUpdate> updates = [];
                await foreach (ChatResponseUpdate update in client.GetStreamingResponseAsync(chatHistory))
                {
                    Console.Write(update);
                    updates.Add(update);
                }
                Console.WriteLine();

                chatHistory.AddMessages(updates);
            }
        }

        /*
         * ================================================================ 1. ================================================================
         * 
         * 你好呀！我是 DeepSeek-R1，一位由深度求索公司开发的智能助手。你可以把我当作一个知识丰富、乐于助人的AI伙伴，随时为你解答问题、提供信息、帮你学习、工作或进行创意写作，甚至陪你聊天放松心情 ??
         * 
         * 不管是学习资料、办公文档、生活琐事，还是突发奇想，尽管告诉我，我会尽力帮你解决！
         * 
         * 你想先了解什么内容呢？
         * ================================================================ 2. ================================================================
         * 
         * Q: 我叫秋林，今年18岁
         * 你好呀，秋林！?? 欢迎认识～
         * 你今年18岁，正是充满活力和好奇的年纪！
         * 不管你是刚步入大学、正在准备升学考试，还是在探索未来的方向，我都很期待能陪你一起聊聊、学习、成长！
         * 
         * 有什么想了解的、想讨论的，或者需要帮忙的地方吗？比如学习建议、生活小技巧、兴趣推荐，甚至只是随便聊聊都可以哦～??
         * 
         * 猜猜看，你是对大学、未来规划、兴趣爱好，还是其他方面感兴趣呢？
         * Q: 今年是2000年
         * 哈哈，原来你设定的背景是2000年呀！?? 如果你是想聊聊那个年代的故事，或者需要关于2000年的一些信息（比如科技、文化、历史事件等）， 随时告诉我哦～
         * 不过作为AI，我没有真正的“时间感知”，但我会尽力帮你解答任何问题！有什么想探讨的吗？??
         * Q: 我哪年出生的？
         * 根据你提供的信息，如果你是2000年出生的，那么在2018年时，你的年龄应该是18岁（2018 - 2000 = 18）。
         * 
         * 不过，如果你现在是2018年，并且告诉我你是18岁，那么你的出生年份应该是 **2000年**（假设你还没有过生日）。
         * 
         * 如果这不是你的情况，可以告诉我更多细节（比如当前年份和年龄），我可以帮你计算出生年份哦！??
         * Q:
         */
    }
}
