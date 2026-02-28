using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Embeddings;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._04._Embedding
{
    public class EmbeddingTester : InterfaceEmbeddingTester
    {
        public async Task Test()
        {
            string apiKey = "abc";
            string baseUrl = "http://192.168.1.211:11434/v1";
            string model = "qwen3-embedding:0.6b";

            EmbeddingClient client = (new OpenAIClient(new ApiKeyCredential(apiKey), new OpenAIClientOptions { Endpoint = new Uri(baseUrl) })).GetEmbeddingClient(model);

            // 测试用的10个“知识”
            string[] texts = ["C# is a popular programming language for data science and machine learning.",
                              "I love cooking Italian pasta with fresh tomatoes and basil.",
                              "The football match was exciting, with the final score being 3-2.",
                              "Machine learning algorithms can identify patterns in large datasets.",
                              "The recipe calls for two cups of flour and three eggs.",
                              "Basketball requires good coordination and teamwork skills.",
                              "Neural networks are inspired by biological brain structures.",
                              "Baking bread at home requires patience and the right temperature.",
                              "The soccer team won the championship after months of training.",
                              "Deep learning has revolutionized computer vision and natural language processing."
                             ];
            Console.WriteLine("Generating embeddings for sample texts...\n");

            // 将测试用的10个知识embedding，并存储到List中
            List<(string text, float[] embedding)> textEmbeddings = [];
            foreach (string text in texts)
            {
                ClientResult<OpenAIEmbedding> embeddingResult = await client.GenerateEmbeddingAsync(text);
                float[] embedding = embeddingResult.Value.ToFloats().ToArray();
                textEmbeddings.Add((text, embedding));
                Console.WriteLine($"✓ {text}");
            }
            Console.WriteLine($"\nStored {textEmbeddings.Count} text embeddings (dimension: {textEmbeddings[0].embedding.Length})\n");

            while (true)
            {
                Console.WriteLine("\n" + new string('-', 88));
                Console.Write("\nYou:");
                string? input = Console.ReadLine();
                if (input is null || input.Length == 0)
                {
                    Console.WriteLine("input can not be empty");
                    continue;
                }
                else if (input.ToLower() == "exit" || input.ToLower() == "quit")
                {
                    break;
                }

                // 计算输入的embeding
                ClientResult<OpenAIEmbedding> queryEmbeddingResult = await client.GenerateEmbeddingAsync(input);
                float[] queryEmbedding = queryEmbeddingResult.Value.ToFloats().ToArray();
                Console.WriteLine(queryEmbedding.Length);
                // Console.WriteLine(string.Join(',', queryEmbedding));

                // 计算输入与前面10个“知识”的相似度
                Console.WriteLine($"\nSearching for: \"{input}\"");
                List<(string text, double similarity)> similarities = [];
                foreach (var (text, embedding) in textEmbeddings)
                {
                    double similarity = CalculateCosineSimilarity(queryEmbedding, embedding);
                    similarities.Add((text, similarity));
                }
                List<(string, double)> topResults = similarities.OrderByDescending(x => x.similarity).Take(3).ToList();
                Console.WriteLine("\nTop 3 Most Similar Texts:");
                Console.WriteLine(new string('=', 88));
                for (int i = 0; i < topResults.Count; i++)
                {
                    var (text, similarity) = topResults[i];
                    Console.WriteLine($"\n{i + 1}. Similarity: {similarity:F4} ({similarity * 100:F2}%)");
                    Console.WriteLine($"   Text: {text}");
                }
            }

            // 计算余弦相似度
            static double CalculateCosineSimilarity(float[] vector1, float[] vector2)
            {
                if (vector1.Length != vector2.Length) throw new ArgumentException("vectors must have the same dimension");

                double dotProduct = 0;
                double magnitude1 = 0;
                double magnitude2 = 0;

                int dimension = vector1.Length;
                for (int i = 0; i < dimension; i++)
                {
                    dotProduct += vector1[i] * vector2[i];
                    magnitude1 += vector1[i] * vector1[i];
                    magnitude2 += vector2[i] * vector2[i];
                }

                magnitude1 = Math.Sqrt(magnitude1);
                magnitude2 = Math.Sqrt(magnitude2);

                if (magnitude1 == 0 || magnitude2 == 0) return 0;
                return dotProduct / (magnitude1 * magnitude2);
            }
        }
        /*
         * Hello, World!
         * Generating embeddings for sample texts...
         * 
         * ? C# is a popular programming language for data science and machine learning.
         * ? I love cooking Italian pasta with fresh tomatoes and basil.
         * ? The football match was exciting, with the final score being 3-2.
         * ? Machine learning algorithms can identify patterns in large datasets.
         * ? The recipe calls for two cups of flour and three eggs.
         * ? Basketball requires good coordination and teamwork skills.
         * ? Neural networks are inspired by biological brain structures.
         * ? Baking bread at home requires patience and the right temperature.
         * ? The soccer team won the championship after months of training.
         * ? Deep learning has revolutionized computer vision and natural language processing.
         * 
         * Stored 10 text embeddings (dimension: 1024)
         * 
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * You:what is machine learning
         * 1024
         * 
         * Searching for: "what is machine learning"
         * 
         * Top 3 Most Similar Texts:
         * ========================================================================================
         * 
         * 1. Similarity: 0.6413 (64.13%)
         *    Text: Machine learning algorithms can identify patterns in large datasets.
         * 
         * 2. Similarity: 0.6345 (63.45%)
         *    Text: Deep learning has revolutionized computer vision and natural language processing.
         * 
         * 3. Similarity: 0.5750 (57.50%)
         *    Text: C# is a popular programming language for data science and machine learning.
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * You:what sports do you like
         * 1024
         * 
         * Searching for: "what sports do you like"
         * 
         * Top 3 Most Similar Texts:
         * ========================================================================================
         * 
         * 1. Similarity: 0.5192 (51.92%)
         *    Text: Basketball requires good coordination and teamwork skills.
         * 
         * 2. Similarity: 0.4052 (40.52%)
         *    Text: The soccer team won the championship after months of training.
         * 
         * 3. Similarity: 0.3594 (35.94%)
         *    Text: The football match was exciting, with the final score being 3-2.
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * You:推荐一些食物给我
         * 1024
         * 
         * Searching for: "推荐一些食物给我"
         * 
         * Top 3 Most Similar Texts:
         * ========================================================================================
         * 
         * 1. Similarity: 0.4298 (42.98%)
         *    Text: I love cooking Italian pasta with fresh tomatoes and basil.
         * 
         * 2. Similarity: 0.3500 (35.00%)
         *    Text: The recipe calls for two cups of flour and three eggs.
         * 
         * 3. Similarity: 0.2963 (29.63%)
         *    Text: Baking bread at home requires patience and the right temperature.
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * You:什么是机器学习
         * 1024
         * 
         * Searching for: "什么是机器学习"
         * 
         * Top 3 Most Similar Texts:
         * ========================================================================================
         * 
         * 1. Similarity: 0.5429 (54.29%)
         *    Text: Machine learning algorithms can identify patterns in large datasets.
         * 
         * 2. Similarity: 0.5301 (53.01%)
         *    Text: C# is a popular programming language for data science and machine learning.
         * 
         * 3. Similarity: 0.5293 (52.93%)
         *    Text: Deep learning has revolutionized computer vision and natural language processing.
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * You:waht is machne learning
         * 1024
         * 
         * Searching for: "waht is machne learning"
         * 
         * Top 3 Most Similar Texts:
         * ========================================================================================
         * 
         * 1. Similarity: 0.5979 (59.79%)
         *    Text: Deep learning has revolutionized computer vision and natural language processing.
         * 
         * 2. Similarity: 0.5699 (56.99%)
         *    Text: Machine learning algorithms can identify patterns in large datasets.
         * 
         * 3. Similarity: 0.5136 (51.36%)
         *    Text: C# is a popular programming language for data science and machine learning.
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * You:exit
         */
    }
}
