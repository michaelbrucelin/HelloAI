using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._01._手动调用AI接口
{
    public class OllamaTester : InterfaceOllamaTester
    {
        public async Task Test()
        {
            OllamaClient ollama = new OllamaClient("http://192.168.1.211:11434");

            // 示例1: 简单生成
            Console.WriteLine("\n======== 简单生成 ========");
            string response = await ollama.GenerateAsync("gpt-oss:20b", "你好，简明扼要的介绍一下你自己");
            Console.WriteLine(response);

            // 示例2: 流式生成
            Console.WriteLine("\n======== 流式生成 ========");
            await ollama.GenerateStreamAsync("gpt-oss:20b", "你好，简明扼要的介绍一下你自己", chunk =>
            {
                Console.Write(chunk);
            });
            Console.WriteLine();

            // 示例3: 聊天对话
            Console.WriteLine("\n======== 聊天对话 ========");
            ChatMessage[] messages = new[]
            {
                new ChatMessage { role = "system", content = "你是一个友好的AI助手，回答问题简明扼要，非必要很粗大都在100字之内" },
                new ChatMessage { role = "user", content = "什么是机器学习?" }
            };
            string chatResponse = await ollama.ChatAsync("gpt-oss:20b", messages, 0.2, 100);
            Console.WriteLine(chatResponse);

            // 示例4: 获取模型列表
            Console.WriteLine("\n======== 可用模型 ========");
            string[] models = await ollama.ListModelsAsync();
            foreach (var model in models)
            {
                Console.WriteLine($"- {model}");
            }
        }

        /*
         * Hello, World!
         * 
         * ======== 简单生成 ========
         * 你好！我是 ChatGPT，OpenAI 训练的大型语言模型，擅长理解和生成自然语言。我的知识截至 2024?06，能够帮助解答问题、撰写文本、进行对 话、提供学习与工作建议等。无论你是想聊聊天、找资料还是做创意写作，我都很乐意协助！
         * 
         * ======== 流式生成 ========
         * 你好！我是 ChatGPT，基于 OpenAI 的 GPT?4 架构的语言模型，能够理解和生成中文（及多种语言），擅长回答问题、写作、翻译、编程、创意 写作等多种任务。我的知识截至 2024 年 6 月，随时准备为你提供帮助。
         * 
         * ======== 聊天对话 ========
         * 机器学习是让计算机通过数据学习模式和规律，自动改进性能的技术。它使用算法在训练集上寻找规律，然后在新数据上做预测或决策。
         * 
         * ======== 可用模型 ========
         * - deepseek-r1:8b
         * - deepseek-r1:1.5b
         * - qwen3-vl:32b
         * - gemma3:27b
         * - qwen3:32b
         * - gpt-oss:20b
         * - deepseek-r1:32b
         */
    }
}
