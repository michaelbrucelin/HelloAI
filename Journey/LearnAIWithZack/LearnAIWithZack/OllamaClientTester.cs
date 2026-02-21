using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack
{
    public class OllamaClientTester
    {
        public async Task Test()
        {
            var ollama = new OllamaClient("http://192.168.1.211:11434");

            // 示例1: 简单生成
            Console.WriteLine("======== 简单生成 ========");
            var response = await ollama.GenerateAsync("gpt-oss:20b", "你好，介绍一下你自己");
            Console.WriteLine(response);

            // 示例2: 流式生成
            Console.WriteLine("\n======== 流式生成 ========");
            await ollama.GenerateStreamAsync("gpt-oss:20b", "你好，介绍一下你自己", chunk =>
            {
                Console.Write(chunk);
            });
            Console.WriteLine();

            // 示例3: 聊天对话
            Console.WriteLine("\n======== 聊天对话 ========");
            var messages = new[]
            {
                new ChatMessage { role = "system", content = "你是一个友好的AI助手，回答问题简明扼要，非必要很粗大都在100字之内" },
                new ChatMessage { role = "user", content = "什么是机器学习?" }
            };
            var chatResponse = await ollama.ChatAsync("gpt-oss:20b", messages, 0.2, 100);
            Console.WriteLine(chatResponse);

            // 示例4: 获取模型列表
            Console.WriteLine("\n======== 可用模型 ========");
            var models = await ollama.ListModelsAsync();
            foreach (var model in models)
            {
                Console.WriteLine($"- {model}");
            }
        }

        /*
        Hello, World!
        ======== 简单生成 ========
        你好！我是 ChatGPT，一个由 OpenAI 训练的大型语言模型。我的主要职责是理解和生成自然语言文本，帮助回答问题、提供信息、撰写文章、翻译、编程、做学习辅导、甚至一起聊聊趣味话题。我的知识库截至 2024 年 6 月，能够覆盖各类常识、学术、技术、文化、娱乐等方面的内容。 虽然我不具备个人经验或情感，但我会尽力用最合适、最友好的方式与你交流。
        
        如果你有任何问题、需要帮助的地方，随时告诉我！无论是学习、工作、兴趣爱好，还是日常小困扰，我都乐意为你服务。欢迎聊聊你想了解的内容吧！
        
        ======== 流式生成 ========
        你好！我是 ChatGPT，一款由 OpenAI 开发的大型语言模型（基于 GPT?4 架构）。我能够理解和生成多种语言的文本，帮助你完成写作、翻译、 回答问题、创意灵感、学习辅导等任务。你可以把我当作一个随时待命、永不疲倦的知识助手，随时为你提供信息、建议或交流想法。有什么想聊的、想做的，随时告诉我吧！
        
        ======== 聊天对话 ========
        机器学习是人工智能的一个分支，利用算法让计算机从数据中自动识别模式并做出预测或决策。它不需要人工编写每一步的规则，而是让模型在训练过程中根据经验不断调整参数，从而在新数据上表现出更好的性能。
        
        ======== 可用模型 ========
        - deepseek-r1:8b
        - deepseek-r1:1.5b
        - qwen3-vl:32b
        - gemma3:27b
        - qwen3:32b
        - gpt-oss:20b
        - deepseek-r1:32b
        */
    }
}
