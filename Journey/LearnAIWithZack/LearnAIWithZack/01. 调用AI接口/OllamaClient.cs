using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearnAIWithZack
{
    public class OllamaClient
    {
        public OllamaClient(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        /// <summary>
        /// 生成文本响应
        /// </summary>
        /// <param name="model"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public async Task<string> GenerateAsync(string model, string prompt, double temperature = 0.5, int max_tokens = 100)
        {
            var requestBody = new
            {
                model,
                prompt,
                stream = false,
                temperature,
                max_tokens
            };

            string json = JsonSerializer.Serialize(requestBody);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/api/generate", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            OllamaResponse result = JsonSerializer.Deserialize<OllamaResponse>(responseBody, options);

            return result != null ? result.response : string.Empty;
        }

        /// <summary>
        /// 流式生成文本响应
        /// </summary>
        /// <param name="model"></param>
        /// <param name="prompt"></param>
        /// <param name="onChunk"></param>
        /// <returns></returns>
        public async Task GenerateStreamAsync(string model, string prompt, Action<string> onChunk, double temperature = 0.5, int max_tokens = 100)
        {
            var requestBody = new
            {
                model,
                prompt,
                stream = true,
                temperature,
                max_tokens
            };

            string json = JsonSerializer.Serialize(requestBody);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/api/generate", content);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new System.IO.StreamReader(stream);

            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    OllamaResponse chunk = JsonSerializer.Deserialize<OllamaResponse>(line);
                    if (chunk?.response != null)
                    {
                        onChunk(chunk.response);
                    }
                }
            }
        }

        /// <summary>
        /// 聊天对话
        /// </summary>
        /// <param name="model"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task<string> ChatAsync(string model, ChatMessage[] messages, double temperature = 0.5, int max_tokens = 100)
        {
            var requestBody = new
            {
                model,
                messages,
                stream = false,
                temperature,
                max_tokens
            };

            string json = JsonSerializer.Serialize(requestBody);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/api/chat", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            OllamaChatResponse result = JsonSerializer.Deserialize<OllamaChatResponse>(responseBody);

            return result?.message?.content ?? string.Empty;
        }

        // 获取可用模型列表
        public async Task<string[]> ListModelsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/api/tags");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            OllamaModelsResponse result = JsonSerializer.Deserialize<OllamaModelsResponse>(responseBody);

            return result?.models?.Select(m => m.name).ToArray() ?? Array.Empty<string>();
        }
    }

    /// <summary>
    /// 响应模型
    /// </summary>
    public class OllamaResponse
    {
        public string model { get; set; }
        public string response { get; set; }
        public bool done { get; set; }
    }

    public class OllamaChatResponse
    {
        public string model { get; set; }
        public ChatMessage message { get; set; }
        public bool done { get; set; }
    }

    public class ChatMessage
    {
        public string role { get; set; }  // "system", "user", "assistant"
        public string content { get; set; }
    }

    public class OllamaModelsResponse
    {
        public ModelInfo[] models { get; set; }
    }

    public class ModelInfo
    {
        public string name { get; set; }
        public DateTime modified_at { get; set; }
        public long size { get; set; }
    }
}