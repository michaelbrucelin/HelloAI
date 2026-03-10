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
        public async Task<float[]> GetEmbeddingAsync(string input, CancellationToken cancellationToken = default)
        {
            OpenAIClient client = new(new ApiKeyCredential(apiKey), new OpenAIClientOptions { Endpoint = new Uri(url) });

            ClientResult<OpenAIEmbedding> embeddingResult = await client.GetEmbeddingClient(model).GenerateEmbeddingAsync(input, cancellationToken: cancellationToken);

            if (embeddingResult.Value != null)
            {
                float[] embedding = embeddingResult.Value.ToFloats().ToArray();
                return embedding;
            }

            throw new Exception("Failed to generate embedding or received null value.");
        }
    }
}
