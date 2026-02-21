using HttpMataki.NET.Auto;
using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._02._使用包调用AI接口
{
    public class OpenAITester03 : InterfaceOpenAITester
    {
        /// <summary>
        /// 测试流式输出，借助HttpMataki.NET.Auto查看http响应的过程
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            // 配置
            string apiKey = "abc";                             // apikey随便写，但是不能为空
            string baseUrl = "http://192.168.1.211:11434/v1";  // 兼容OpenAI的格式是：http://host:11434/v1/chat/completions，sdk会自动补全/chat/completions
            string model = "gpt-oss:20b";

            // 示例1: 

            HttpClientAutoInterceptor.StartInterception();     // 类似于tcpdump，查看项目运行过程中全部的http请求和响应的细节，输出到控制台

            // 消息
            ChatMessage[] messages = [
                new ChatMessage (ChatRole.System, "你是一个AI助手。"),
                new ChatMessage (ChatRole.User,"给我讲一个关于人工智能的小故事，大约100个字。")
            ];

            // 创建客户端
            IChatClient client = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey), new OpenAIClientOptions() { Endpoint = new Uri(baseUrl) }).AsIChatClient();

            // 流式输出
            // ChatResponse response = await client.GetResponseAsync(messages);
            Console.WriteLine();
            await foreach (ChatResponseUpdate update in client.GetStreamingResponseAsync(messages)) Console.Write(update.Text);

            Console.WriteLine($"\n输出完成。");
        }
        /*
         * Hello, World!
         * [HttpMataki.NET.Auto] HttpClient automatic interception started
         * 
         * 2026-02-21 17:04:38 - Request:
         * 
         * Method: POST
         * 
         * URL: http://192.168.1.211:11434/v1/chat/completions
         * 
         * Headers:
         * 
         *   Accept: application/json
         * 
         *   User-Agent: OpenAI/2.8.0, (.NET 8.0.24; Microsoft Windows 10.0.19045), MEAI/10.3.0
         * 
         *   Authorization: Bearer abc
         * 
         *   Content-Type: application/json
         * 
         * Body: {"messages":[{"role":"system","content":"你是一个AI助手。"},{"role":"user","content":"给我讲一个关于人工智能的小故事，大约100个字。"}],"model":"gpt-oss:20b","stream":true,"stream_options":{"include_usage":true}}
         * 
         * 2026-02-21 17:04:39 - Response:
         * 
         * Status Code: 200 OK
         * 
         * Headers:
         * 
         *   Date: Sat, 21 Feb 2026 09:04:39 GMT
         * 
         *   Transfer-Encoding: chunked
         * 
         *   Content-Type: text/event-stream
         * 
         * Body: data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"User"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" They"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" want"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" a"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" story"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" about"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" artificial"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" intelligence"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":","},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" about"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" They"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" said"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"大"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"约"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"字"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" meaning"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" approximately"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Should"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" produce"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" sentence"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"(s"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":")."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Should"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" be"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" around"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" They"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" want"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" a"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" little"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" story"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":","},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" about"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" AI"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"We"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" need"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" approximate"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" including"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" punctuation"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"?"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" They"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" want"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" approximate"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" craft"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" a"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" story"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" that"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" is"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" about"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" try"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" We"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" can"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" produce"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" one"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" or"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" two"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" paragraphs"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" produce"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"90"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"-"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"110"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" draft"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"未来"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"助手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"帮助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"整理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事务"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"决定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"为"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"找"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"随着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"陪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"伴"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"下"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"逐"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"渐"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"恢复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"开始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"短"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"短"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664679,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"段"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"时间"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"笑"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"声"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"更"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"频"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"繁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"光"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"提醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，而"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Need"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" character"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" We'll"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" manually"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Use"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" counting"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" punctuation"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" We'll"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" each"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" character"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":","},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" punctuation"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" is"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" counted"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" as"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"1"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" but"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" we"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ignore"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"?"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" We'll"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" break"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" into"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" tokens"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"未来"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"助手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"帮助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"整理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事务"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"决定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"为"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"找"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"随着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"陪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"伴"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"下"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"逐"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"渐"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"恢复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"开始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"短"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"短"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"段"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"时间"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"笑"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"声"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"更"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"频"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"繁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"光"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"提醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，而"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\"\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" I'll"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ("},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"including"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" punctuation"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"?)."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Usually"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Chinese"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" include"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" punctuation"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" as"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" separate"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" char"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" But"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" approximate"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664680,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"I'll"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" recount"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"1"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"2"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"未"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"3"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 来"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"4"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"5"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 城"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"6"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"7"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"8"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"9"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"10"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"11"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"12"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"13"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"14"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"15"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"16"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 帮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"17"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"18"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"19"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"20"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 整"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"21"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"22"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"23"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"24"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"25"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 宾"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"26"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"27"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"28"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"29"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"30"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"31"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"32"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"33"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"34"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"35"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"36"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"37"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"38"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"39"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"40"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"41"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"42"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 决"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"43"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"44"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 为"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"45"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"46"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 找"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"47"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"48"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"49"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"50"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"51"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"52"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"53"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"54"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 随"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"55"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"56"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"57"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"58"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"59"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"60"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 陪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"61"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 带"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"62"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 下"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"63"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664681,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"逐"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"64"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 渐"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"65"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 恢"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"66"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"67"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"68"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"69"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"70"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"71"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"72"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"73"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 开"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"74"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"75"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"76"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"觉"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"77"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"78"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 科"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"79"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"80"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"81"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"82"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"83"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"84"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"85"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"86"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"87"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"88"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"89"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 短"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"90"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 短"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"91"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"92"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 段"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"93"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 时"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"94"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 间"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"95"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"96"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 城"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"97"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"98"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"99"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 笑"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 声"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"101"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 更"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"102"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"频"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"103"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 繁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"104"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"105"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"106"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"107"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"108"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"109"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"110"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"111"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"112"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"113"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"114"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 光"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"115"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"116"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 提"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"117"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"118"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"119"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"120"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"121"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 科"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"122"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"123"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"124"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664682,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"125"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"126"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"127"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 而"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"128"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"129"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"130"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"131"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"132"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"133"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"134"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"135"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Total"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"135"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" We"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" want"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" about"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Could"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" trim"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" cut"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" down"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" aim"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" for"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" around"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"-"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"110"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Remove"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" some"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" sentences"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" produce"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"4"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" or"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"5"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" sentences"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" produce"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"未来"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"助手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"帮助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"整理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事务"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"决定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"为"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"找"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"随着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"陪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"伴"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"下"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"恢复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"开始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"笑"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"声"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"更"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"频"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"繁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"提醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，而"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" again"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"1"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"2"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 未"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"3"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 来"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"4"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"5"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 城"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"6"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"7"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"8"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"9"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"10"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664683,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"11"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"12"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"13"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"14"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"15"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"16"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 帮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"17"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"18"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"19"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"20"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 整"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"21"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"22"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"23"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"24"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"25"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 宾"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"26"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"27"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"28"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"29"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"30"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"31"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"32"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"33"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"34"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"35"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"36"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"37"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"38"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"39"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"40"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"41"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"42"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 决"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"43"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"44"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 为"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"45"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"46"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 找"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"47"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"48"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"49"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"50"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"51"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"52"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"53"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"54"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 随"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"55"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"56"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"57"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"58"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"59"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"60"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 陪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"61"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 带"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"62"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 下"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"63"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 恢"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"64"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"65"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"66"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"67"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"68"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"69"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"70"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 开"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"71"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"72"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"73"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664684,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"觉"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"74"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"75"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 科"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"76"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"77"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"78"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"79"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"80"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"81"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"82"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"83"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"84"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"85"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"86"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 城"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"87"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"88"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"89"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 笑"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"90"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 声"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"91"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 更"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"92"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"频"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"93"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 繁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"94"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"95"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"96"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"97"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"98"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"99"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"101"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"102"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"103"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"104"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 提"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"105"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"106"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"107"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"108"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"109"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 科"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"110"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"111"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"112"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"113"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"114"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"115"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 而"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"116"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"117"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"118"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"119"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"120"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"We"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" have"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"120"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" We"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" can"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" trim"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" again"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" to"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" about"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" cut"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" some"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" details"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Remove"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"未来"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"城市"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" phrase"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" or"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" repeated"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Let's"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" cut"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" last"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" part"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" maybe"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Re"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"vised"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \""},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"助手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"每天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"整理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664685,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事务"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"决定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"给"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"一个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"随着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"恢复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"开始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"力量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"提醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\"\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Count"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":":\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"1"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"2"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"3"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"4"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"5"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 助"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"6"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"7"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"8"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"9"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 每"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"10"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"11"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 整"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"12"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"13"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"14"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"15"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"16"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"17"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"18"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"事"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"19"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"物"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"20"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"21"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"22"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"23"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"24"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"25"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"26"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"27"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"28"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"29"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"30"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"31"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"32"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"33"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"34"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"35"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"36"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 决"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"37"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"38"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 给"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"39"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"40"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"41"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"42"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664686,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"43"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"44"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"45"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"46"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"47"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 随"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"48"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"49"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"50"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 恢"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"51"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"52"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"53"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"54"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"55"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"56"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"57"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 开"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"58"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"59"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"60"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"觉"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"61"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"62"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 科"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"63"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"64"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"65"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"66"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"67"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"68"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"69"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"70"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"71"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"72"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"73"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"74"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"75"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"76"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"77"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"78"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"79"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"80"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"81"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"82"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 提"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"83"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"84"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"85"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"86"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"87"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 科"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"88"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"89"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"90"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"91"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"92"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" ，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"93"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"94"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"95"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"96"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"97"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" 。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" \n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"We"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" have"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"97"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" characters"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Good"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"97"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" within"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" approx"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" "},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"100"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" Perfect"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":".\n\n"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"Thus"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" output"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":" that"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664687,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"","reasoning":"."},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"人工"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"智能"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"助手"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"小"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"智"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"每天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"整理"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"日"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"常"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"事务"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"一天"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"，它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"在"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"街"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"角"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"遇"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"一"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"只"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"流"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"浪"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"决定"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"给"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"它"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"一个"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"温"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"暖"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"家"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"随着"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"猫"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"恢复"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"活"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"力"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"，人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"开始"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"意识"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"到"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"与"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"善"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"意"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"并"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"重"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"的"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"力量"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"连"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"路"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"也"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"悄"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"亮"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"起"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"提醒"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"人"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"们"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"："},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"科技"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"桥"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"梁"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"，"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"爱"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"是"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"灯"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"塔"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":"。"},"finish_reason":null}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"delta":{"role":"assistant","content":""},"finish_reason":"stop"}]}
         * 
         * data: {"id":"chatcmpl-139","object":"chat.completion.chunk","created":1771664688,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[],"usage":{"prompt_tokens":96,"completion_tokens":2017,"total_tokens":2113}}
         * 
         * data: [DONE]
         * 
         * 
         * 
         * **************************************************
         * 
         * 人工智能助手小智每天整理人们的日常事务。一天，它在街角遇到一只流浪猫，决定给它一个温暖的家。随着猫恢复活力，人们开始意识到科技与善意并重的力量。连路灯也悄悄亮起，提醒人们：科技是桥梁，爱是灯塔。
         * 输出完成。
         */
    }
}
