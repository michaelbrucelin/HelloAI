using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Embeddings;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._06._RAG
{
    public class EmbeddingClient(string url, string model, string apiKey = null)
    {
        private OpenAI.Embeddings.EmbeddingClient CreateClient()
        {
            return (new OpenAIClient(new ApiKeyCredential(apiKey), new OpenAIClientOptions { Endpoint = new Uri(url) })).GetEmbeddingClient(model);
        }

        public async Task<float[]> GetEmbeddingAsync(string input, CancellationToken cancellationToken = default)
        {
            OpenAI.Embeddings.EmbeddingClient client = CreateClient();

            ClientResult<OpenAIEmbedding> embeddingResult = await client.GenerateEmbeddingAsync(input, cancellationToken: cancellationToken);
            if (embeddingResult.Value != null)
            {
                float[] embedding = embeddingResult.Value.ToFloats().ToArray();
                return embedding;
            }

            throw new Exception("Failed to generate embedding or received null value.");
        }
    }
}
