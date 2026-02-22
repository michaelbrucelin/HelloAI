using MemNet;
using MemNet.Abstractions;
using MemNet.Config;
using MemNet.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAIWithZack._03._AI是无状态的
{
    public class AIStateTester03 : InterfaceAIStateTester
    {
        /// <summary>
        /// 使用杨中科老师的MemNet包来实现AI的记忆层
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")            // NuGet Package: Microsoft.Extensions.Configuration.Json
                .AddEnvironmentVariables()                  // NuGet Package: Microsoft.Extensions.Configuration.EnvironmentVariables
                .Build();

            var services = new ServiceCollection();
            services.AddMemNet(configuration);

            await using var serviceProvider = services.BuildServiceProvider();
            var memoryService = serviceProvider.GetRequiredService<IMemoryService>();
            var memoryConfig = serviceProvider.GetRequiredService<IOptions<MemoryConfig>>().Value;
            await memoryService.InitializeAsync();

            var chatApiKey = memoryConfig.LLM.ApiKey;
            var textGenEndpoint = memoryConfig.LLM.Endpoint;
            var textGenDeploymentName = memoryConfig.LLM.Model;
            var client = new CompleteChatClient(textGenEndpoint, textGenDeploymentName, chatApiKey);

            while (true)
            {
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

                List<MemorySearchResult> memorys = await memoryService.SearchAsync(new SearchMemoryRequest { Query = input, UserId = "user001" });

                string memory = string.Join('\n', memorys.Select(e => e.Memory.UpdatedAt + ":" + e.Memory.Data));
                Console.WriteLine("Memory:");
                Console.WriteLine(memory);
                string answer = await client.GenerateTextAsync(input, memory);
                Console.Write("AI：");
                Console.WriteLine(answer);

                // 保存新的记忆
                await memoryService.AddAsync(new AddMemoryRequest
                {
                    Messages =
                    [
                        new MessageContent{Role = "User", Content = input}
                    ],
                    UserId = "user001"
                });
            }
        }
    }
}
