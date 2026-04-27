using Microsoft.Extensions.AI;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Microsoft.com._01.AI工具和SDK._01.Microsoft.Extensions.AI库
{
    public class IChatClient03 : InterfaceIChatClient
    {
        /// <summary>
        /// 工具调用
        /// AIFunction：表示可描述为 AI 模型并调用的函数。
        /// AIFunctionFactory：提供用于创建 AIFunction 表示.NET 方法的实例的工厂方法。
        /// FunctionInvokingChatClient：将IChatClient包装成另一个IChatClient，并添加自动函数调用功能。
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            int id = 0;
            string GetCurrentWeather() => Random.Shared.NextDouble() > -1 ? "sunny, big wind, hot" : "";
            string GetTomorrowWeather() => Random.Shared.NextDouble() > -1 ? "very big rain, no wind, cold" : "";

            IChatClient client = new OllamaApiClient(new Uri(Utils.base_url), Utils.model_chatgpt_20b);
            client = ChatClientBuilderChatClientExtensions
                .AsBuilder(client)
                .UseFunctionInvocation()
                .Build();
            ChatOptions options = new()
            {
                Tools = [
                    AIFunctionFactory.Create(GetCurrentWeather),
                    AIFunctionFactory.Create(GetTomorrowWeather)
                ]
            };

            Utils.WriteSplitLine(++id);
            ChatMessage[] messages = [
                new(ChatRole.System, "You are a helpful AI assistant, 并且用简体中文回答问题"),
                new(ChatRole.User, "我现在想去爬山，给我穿衣计划。")
            ];
            IAsyncEnumerable<ChatResponseUpdate> response = client.GetStreamingResponseAsync(messages, options);
            await foreach (ChatResponseUpdate update in response)
            {
                Console.Write(update);
            }

            Utils.WriteSplitLine(++id);
            messages = [
                new(ChatRole.System, "You are a helpful AI assistant, 并且用简体中文回答问题"),
                new(ChatRole.User, "我明天想去骑行，给我穿衣计划。")
            ];
            response = client.GetStreamingResponseAsync(messages, options);
            await foreach (ChatResponseUpdate update in response)
            {
                Console.Write(update);
            }
        }

        /*
         * ================================================================ 1. ================================================================
         * 
         * 好的，我可以帮你制定一份爬山穿衣计划。为了给你提供最合适的建议，我需要了解以下信息：
         * 
         * 1. **你计划去的山地或地点**（城市/省份/具体山峰）
         * 2. **计划的日期或季节**（春季、夏季、秋季、冬季或具体时间）
         * 3. **天气预报**（你想知道当天或未来几天的温度、降水概率、风速等，或者你想我帮你查一下）
         * 4. **你个人的体力与对温度的耐受程度**（比如你是怕热还是怕冷）
         * 5. **登山的时长与难度**（短途徒步还是长时间高海拔攀登）
         * 
         * 如果你方便提供上述信息，我可以给出更精准的穿衣建议。
         * 如果你想先得到一个通用的“春季/夏季/秋季/冬季”示例穿衣清单，我也可以先提供。请告诉我你更倾向哪种方式吧！
         * 
         * ================================================================ 2. ================================================================
         * 
         * **明天骑行穿衣计划（大雨、无风、寒冷）**
         * 
         * | 层次 | 建议穿着 | 说明 |
         * |------|----------|------|
         * | **内层（基础层）** | ? 速干保暖内衣（长袖）<br>? 速干打底裤 | 维持体温，吸汗排湿，防止被雨水打湿后变冷。 |
         * | **中层（保温层）** | ? 轻薄抓绒或羊毛衫（可根据体感温度增减） | 提供核心保暖，避免体表过冷。 |
         * | **外层（防护层）** | ? 防水透气外套（最好带帽子）<br>? 防水骑行裤（或加一层防水雨衣） | 防止雨水渗入，保持干燥；帽子可防雨水滴落。 |
         * | **鞋子** | ? 防水登山鞋或专用防水骑行鞋 | 防止脚部被雨水浸湿，保持脚部温暖。 |
         * | **配件** | ? 防水手套（加厚）<br>? 防水围巾或颈套<br>? 眼镜（防雾且防雨）<br>? 背包防水罩 | 手部和颈部同样容易失温，眼镜防雾可避免视线模糊。 |
         * | **可选** | ? 温度计或温度提醒装置 <br>? 热水瓶（可放入热水） | 防寒和随时补水。 |
         * 
         * ### 细节建议
         * 
         * 1. **层次叠穿**：由于没有风，寒冷主要来源于低温和雨水，建议尽量多穿一层基础保暖层，避免一次穿得太厚导致出汗后变得湿冷。
         * 2. **防水措施**：外层和鞋子都需要防水，雨水若能被及时排除，身体热量消耗会降低。
         * 3. **防滑**：雨天地面湿滑，可考虑在骑行鞋底加装防滑垫或选择防滑轮胎。
         * 4. **携带雨具**：如果你想换衣服，携带一个小型雨伞或防水袋，方便在途中快速更换或储物。
         * 5. **安全**：雨天能见度低，记得佩戴反光背心或骑行灯，确保行车安全。
         * 
         * 祝你骑行愉快且安全！如果还有其他需求（如路线建议或装备清单），随时告诉我。
         */
    }
}
