using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._06._RAG
{
    public class ChatClient(string endpoint, string deploymentName, string apiKey = null)
    {
        private IChatClient CreateClient()
        {
            return new OpenAI.Chat.ChatClient(model: deploymentName, credential: new ApiKeyCredential(apiKey), options: new OpenAIClientOptions { Endpoint = new Uri($"{endpoint}") }).AsIChatClient();
        }

        public async Task<string> GenerateTextAsync(string input, string context, CancellationToken cancellationToken = default)
        {
            using IChatClient client = CreateClient();

            List<ChatMessage> messages = new List<ChatMessage>{
                new ChatMessage(ChatRole.System, $"根据提供的内容使用简体中文回答用户的问题。注意：不要在提供的内容之外进行回答，回答简明扼要，控制在100字之内。内容如下：\n{context}。"),
                new ChatMessage(ChatRole.User, input)
            };
            ChatResponse response = await client.GetResponseAsync(messages, cancellationToken: cancellationToken);

            return response.Text ?? string.Empty;
        }

        public async IAsyncEnumerable<string> GenerateStreamingTextAsync(string input, string context, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using IChatClient client = CreateClient();

            List<ChatMessage> messages = new List<ChatMessage>        {
                new ChatMessage(ChatRole.System, $"根据提供的内容使用简体中文回答用户的问题。注意：不要在提供的内容之外进行回答，回答简明扼要，控制在100字之内。内容如下：\n{context}。"),
                new ChatMessage(ChatRole.User, input)
            };

            await foreach (ChatResponseUpdate update in client.GetStreamingResponseAsync(messages, cancellationToken: cancellationToken))
            {
                yield return update.Text ?? string.Empty;
            }
        }
    }
}
