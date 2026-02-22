using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._03._AI是无状态的
{
    public class CompleteChatClient(string endpoint, string deploymentName, string apiKey = null)
    {
        public async Task<string> GenerateTextAsync(string input, string memory, CancellationToken cancellationToken = default)
        {
            ChatClient client = new(deploymentName, new ApiKeyCredential(apiKey), new OpenAIClientOptions { Endpoint = new Uri($"{endpoint}") });

            ChatCompletion completion = await client.CompleteChatAsync(
                [
                    new SystemChatMessage($"Answer the user's question based on the provided content. Below is the memory about this user:\n{memory}"),
                    new UserChatMessage(input)
                ],
                cancellationToken: cancellationToken);

            StringBuilder sb = new StringBuilder();
            foreach (var content in completion.Content)
            {
                string message = content.Text;
                sb.AppendLine(message);
            }

            return sb.ToString();
        }

        public async IAsyncEnumerable<string> GenerateStreamingTextAsync(string input, string memory, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ChatClient client = new(deploymentName, new ApiKeyCredential(apiKey), new OpenAIClientOptions { Endpoint = new Uri($"{endpoint}") });

            var async_completion = client.CompleteChatStreamingAsync(
                [
                    new SystemChatMessage($"Answer the user's question based on the provided content. Below is the memory about this user: \n{memory}"),
                    new UserChatMessage(input)
                ],
                cancellationToken: cancellationToken);

            await foreach (var update in async_completion) foreach (var content in update.ContentUpdate)
                {
                    yield return content.Text ?? string.Empty;
                }
        }
    }
}
