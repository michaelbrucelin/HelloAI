using HttpMataki.NET.Auto;
using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._07._FunctionCalling
{
    public class FunctionCallingTester03 : InterfaceFunctionCallingTester
    {
        public async Task Test()
        {
            HttpClientAutoInterceptor.StartInterception();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string apiKey = "abc";
            string baseUrl = "http://192.168.1.211:11434/v1";
            string model = "gpt-oss:20b";

            ChatClient03 chatClient = new ChatClient03(baseUrl, model, apiKey);

            string response = await chatClient.GenerateWithFunctionCallingAsync("Find all my password files under E:\\主同步盘\\我的坚果云\\个人资料\\网络帐号, and write the result to E:\\temp\\password_files.txt");
            Console.WriteLine(response);
        }
    }

    public class ChatClient03(string url, string model, string apiKey = "")
    {
        public async Task<string> GenerateWithFunctionCallingAsync(string input, CancellationToken cancellationToken = default)
        {
            // Use OpenAI ChatClient and convert to IChatClient
            using IChatClient chatClient = new OpenAI.Chat.ChatClient(model, new ApiKeyCredential(apiKey ?? ""), new OpenAIClientOptions() { Endpoint = new Uri(url) }).AsIChatClient();

            List<ChatMessage> messages = [
                new ChatMessage(ChatRole.System, "You are a helpful assistant that can help users with file operations. You can search for files, read their contents, and write to files. Use these tools to help users manage their files effectively."),
                new ChatMessage(ChatRole.User, input)
            ];

            ChatOptions options = new ChatOptions
            {
                Tools = [
                    AIFunctionFactory.Create(SearchFiles),
                    AIFunctionFactory.Create(ReadTextFile),
                    AIFunctionFactory.Create(WriteToTextFileAsync),
                    AIFunctionFactory.Create(GetAllDrives)
                ]
            };

            // Use ChatClientBuilder with UseFunctionInvocation for automatic function calling
            using IChatClient client = new ChatClientBuilder(chatClient)
                .UseFunctionInvocation()
                // .UseToolReduction(new EmbeddingToolReductionStrategy())
                .Build();
            ChatResponse response = await client.GetResponseAsync(messages, options, cancellationToken);

            return response.Text;
        }

        [Description("Search all given file types under a directory and return matched files' full paths")]
        private string[] SearchFiles([Description("Directory path to search in")] string directory, [Description("Array of file extensions to search for (e.g., ['.txt', '.cs', '.json'])")] string[] extensions)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Directory not found: {directory}");
            }

            List<string> allFiles = new List<string>();

            foreach (string extension in extensions)
            {
                string searchPattern = $"*{extension}";
                string[] files = Directory.GetFiles(directory, searchPattern, SearchOption.AllDirectories);
                allFiles.AddRange(files);
            }

            return allFiles.Distinct().ToArray();
        }

        [Description("Read the text content of a given file")]
        private string ReadTextFile([Description("Full path to the file to read")] string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File not found: {fullPath}");
            }

            return File.ReadAllText(fullPath);
        }

        [Description("Write given content to a text file at the specified path")]
        private async Task WriteToTextFileAsync([Description("Full path where to write the file")] string fullPath, [Description("Content to write to the file")] string content, CancellationToken cancellationToken)
        {
            string directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllTextAsync(fullPath, content, cancellationToken);
        }

        [Description("Get all available drives on the computer with their properties")]
        private object[] GetAllDrives()
        {
            var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady)
                                              .Select(drive => new
                                              {
                                                  drive.Name,
                                                  drive.DriveType,
                                                  TotalSize = drive.TotalSize,
                                                  AvailableSpace = drive.AvailableFreeSpace,
                                                  drive.VolumeLabel
                                              })
                                              .ToArray();

            return drives.ToArray<object>();
        }
    }
}
