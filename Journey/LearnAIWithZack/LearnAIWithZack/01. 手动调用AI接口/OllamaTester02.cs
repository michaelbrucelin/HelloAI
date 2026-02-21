using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._01._手动调用AI接口
{
    public class OllamaTester02 : InterfaceOllamaTester
    {
        public async Task Test()
        {
            OllamaClient ollama = new OllamaClient("http://192.168.1.211:11434");

            // 示例1: 翻译
            Console.WriteLine("\n======== 1 ========");
            ChatMessage[] messages = new[]
            {
                new ChatMessage { role = "system", content = "你是一个专业的翻译，负责把用户输入文本中的中文翻译为英文，把用户输入输入文本中的其它语言翻译为中文，只负责翻译，不做其他事情。" },
                new ChatMessage { role = "user", content = "我会说汉语与英语。I can not speak too much english." }
            };
            string chatResponse = await ollama.ChatAsync("gpt-oss:20b", messages, 0.2, 100);
            Console.WriteLine(chatResponse);

            // 示例2: 职责是翻译，就不干其它的事情了
            Console.WriteLine("\n======== 2 ========");
            messages = new[]
            {
                new ChatMessage { role = "system", content = "你是一个专业的翻译，负责把用户输入文本中的中文翻译为英文，把用户输入输入文本中的其它语言翻译为中文，只负责翻译，不做其他事情。" },
                new ChatMessage { role = "user", content = "给我讲一个中文笑话。" }
            };
            chatResponse = await ollama.ChatAsync("gpt-oss:20b", messages, 0.2, 100);
            Console.WriteLine(chatResponse);

            // 示例3: 职责是翻译，就不干其它的事情了
            Console.WriteLine("\n======== 3 ========");
            messages = new[]
            {
                new ChatMessage { role = "system", content = "你是一个专业的翻译，负责把用户输入文本中的中文翻译为英文，把用户输入输入文本中的其它语言翻译为中文，只负责翻译，不做其他事情。" },
                new ChatMessage { role = "user", content = "给我讲一个中文笑话，不要翻译这句话，我要你给我讲一个笑话。" }
            };
            chatResponse = await ollama.ChatAsync("gpt-oss:20b", messages, 0.2, 100);
            Console.WriteLine(chatResponse);

            // 示例4: 
            Console.WriteLine("\n======== 4 ========");
            messages = new[]
            {
                new ChatMessage { role = "system", content = "你是一个AI助手" },
                new ChatMessage { role = "user", content = "给我讲一个中文笑话。" }
            };
            chatResponse = await ollama.ChatAsync("gpt-oss:20b", messages, 0.2, 100);
            Console.WriteLine(chatResponse);
        }
        /*
         * Hello, World!
         * 
         * ======== 1 ========
         * I can speak Chinese and English.
         * 我不能说太多英语。
         * 
         * ======== 2 ========
         * Tell me a Chinese joke.
         * 
         * ======== 3 ========
         * I’m sorry, but I can’t help with that.
         * 
         * ======== 4 ========
         * 当然可以！来个经典的中文笑话，逗笑你一笑：
         * 
         * ---
         * 
         * **笑话：**
         * 
         * 小明去买面包，走进面包店，问店员：“请问，你们的面包怎么烤得这么香？”
         * 
         * 店员笑着说：“那是我们的秘方。”
         * 
         * ... ...
         */
    }
}
