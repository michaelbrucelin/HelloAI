# README

Function Calling本质上仍然是记忆层。

例如，直接问大模型，今天的大连的天气适合穿什么衣服出门，大模型无法回答，因为大模型不知道今天大连的天气如何，但是如果问的时候把今天大连的天气情况一起发给大模型（本质上与记忆层还有RAG一回事），大模型就可以回答了。

Fucntion Calling本质上就是这么回事，但是可以使用一些框架来自动化这个过程，下面用`Microsoft.Extensions.AI.OpenAI`这个包来描述一下过程，据说MAF（Microsoft Agent Framework）更好用，但是没用过。

1. 请求的时候除了向大模型提供**系统提示词**，**用户提示词**之外，再向大模型提供一个**能力列表**，即**你能做哪些事，以及做某件事对应的函数信息**
2. 大模型先分析**系统提示词**与**用户提示词**
   - 如果可以生成回答，就生成回答并返回结果
   - 如果无法生成回答，就看大模型缺少的信息能否通过**能力列表**中的某个函数去获取，如果可以，大模型就会返回需要本地调用哪些函数
3. `Microsoft.Extensions.AI.OpenAI`框架接收消息并执行大模型返回的函数，然后将函数结果连同前面的**系统提示词**以及**用户提示词**一次发送给大模型
4. 大模型生成结果并返回结果

FunctionCallingTester测试没有FunctionCalling的情况下结果是怎样的
FunctionCallingTester02测试有FunctionCalling的情况下结果是怎样的
FunctionCallingTester03测试有多个FunctionCalling的情况下结果是怎样的

下面是抓取到的Function Calling的http细节，可以看到实际请求了两次，第一次请求，大模型返回了本地需要调用的函数以及函数的参数，第二次请求将本地函数执行的结果放到“助手提示词”中一起发给了大模型

```http
[HttpMataki.NET.Auto] HttpClient automatic interception started

2026-03-13 13:08:40 - Request:
Method: POST
URL: http://192.168.1.211:11434/v1/chat/completions
Headers:
  Accept: application/json
  User-Agent: OpenAI/2.9.1, (.NET 8.0.25; Microsoft Windows 10.0.19045), MEAI/10.4.0
  Authorization: Bearer abc
  Content-Type: application/json
Body: {"messages":[{"role":"system","content":"You are a helpful assistant that can help users with the given function tools."},{"role":"user","content":"大连今天的天气适合于穿什么衣服？"}],"model":"gpt-oss:20b","tools":[{"type":"function","function":{"description":"Get weather information for the specified city","name":"GetWeatherInfo","parameters":{
  "type": "object",
  "required": [
    "city"
  ],
  "properties": {
    "city": {
      "description": "City name, for example: Beijing, Shanghai",
      "type": "string"
    }
  },
  "additionalProperties": false
}}}],"tool_choice":"auto"}


2026-03-13 13:08:44 - Response:
Status Code: 200 OK
Headers:
  Date: Fri, 13 Mar 2026 05:08:44 GMT
  Content-Type: application/json
  Content-Length: 619
Body: {"id":"chatcmpl-366","object":"chat.completion","created":1773378524,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"message":{"role":"assistant","content":"","reasoning":"User asks: \"大连今天的天气适合于穿什么衣服？\" Chinese: \"What clothes should I wear today in Dalian?\" We need weather info. Use GetWeatherInfo function.","tool_calls":[{"id":"call_dl697xwg","index":0,"type":"function","function":{"name":"GetWeatherInfo","arguments":"{\"city\":\"Dalian\"}"}}]},"finish_reason":"tool_calls"}],"usage":{"prompt_tokens":167,"completion_tokens":68,"total_tokens":235}}


********************************************************************************************************************************


2026-03-13 13:08:44 - Request:
Method: POST
URL: http://192.168.1.211:11434/v1/chat/completions
Headers:
  Accept: application/json
  User-Agent: OpenAI/2.9.1, (.NET 8.0.25; Microsoft Windows 10.0.19045), MEAI/10.4.0
  Authorization: Bearer abc
  Content-Type: application/json
Body: {"messages":[{"role":"system","content":"You are a helpful assistant that can help users with the given function tools."},{"role":"user","content":"大连今天的天气适合于穿什么衣服？"},{"role":"assistant","content":"","tool_calls":[{"id":"call_dl697xwg","type":"function","function":{"name":"GetWeatherInfo","arguments":"{\r\n  \"city\": \"Dalian\"\r\n}"}}]},{"role":"tool","tool_call_id":"call_dl697xwg","content":"\"{\\\"city\\\":\\\"Dalian\\\",\\\"temperature\\\":\\\"22\\\\u00B0C\\\",\\\"condition\\\":\\\"Sunny\\\",\\\"humidity\\\":\\\"65%\\\",\\\"windSpeed\\\":\\\"10 km/h\\\"}\""}],"model":"gpt-oss:20b","tools":[{"type":"function","function":{"description":"Get weather information for the specified city","name":"GetWeatherInfo","parameters":{
  "type": "object",
  "required": [
    "city"
  ],
  "properties": {
    "city": {
      "description": "City name, for example: Beijing, Shanghai",
      "type": "string"
    }
  },
  "additionalProperties": false
}}}],"tool_choice":"auto"}


2026-03-13 13:08:47 - Response:
Status Code: 200 OK
Headers:
  Date: Fri, 13 Mar 2026 05:08:47 GMT
  Content-Type: application/json
  Content-Length: 1996
Body: {"id":"chatcmpl-41","object":"chat.completion","created":1773378527,"model":"gpt-oss:20b","system_fingerprint":"fp_ollama","choices":[{"index":0,"message":{"role":"assistant","content":"大连今天的气温在?22?℃，晴朗、湿度适中、风速约?10?km/h，属于**温暖、微凉**的天气。建议穿以下组合：\n\n| 场景 | 服装建议 |\n|------|----------|\n| **白天** | 轻薄 T?恤 或短袖衬衫 + 轻薄长裤/休 闲短裤，配上 **薄款风衣/长袖衬衫**（如果你对晚间降温较敏感）。 |\n| **晚间/傍晚** | 由于风速不大但温度可能略低，可备一件**薄款针织衫**或**轻柔连帽外套**。 |\n| **室内外** | 若室内温度高（如空调房），可穿长袖或加件轻薄外套，方便随时取下。 |\n| **鞋子** | 运动鞋/帆布鞋（透气、舒适），如果你要去海边或海风较大，可以选择 **防水/防滑的轻质运动鞋**。 |\n\n**小贴士**  \n- 太阳直射时记得戴 墨镜、涂防晒霜；  \n- 夜间气温可能比白天低约 5?℃，可随身携带一件轻薄外套，方便调节；  \n- 如果你计划进行户外运动，建议准备一套**速干、吸汗**的运动服。\n\n总的来说，**轻薄、层叠式穿搭**既能应对上午的温暖，又能在傍晚微凉时保持舒适。祝你今天玩得愉快！","reasoning":"We have a weather API: returned temperature 22°C, sunny, humidity 65%, wind speed 10 km/h.\n\nThe user: \"大连今天的天 气适合于穿什么衣服？\" meaning: \"What clothes are suitable for today's weather in Dalian?\" So we should respond with advice: 22°C, sunny, moderate humidity, mild wind. So light clothing: T-shirt, short sleeves, maybe light jacket if evening cool. Maybe recommended: short sleeves, light sweater or long pants, comfortable shoes, etc.\n\nWe should comply, answer in Chinese. Provide suggestions."},"finish_reason":"stop"}],"usage":{"prompt_tokens":233,"completion_tokens":510,"total_tokens":743}}


********************************************************************************************************************************


大连今天的气温在?22?℃，晴朗、湿度适中、风速约?10?km/h，属于**温暖、微凉**的天气。建议穿以下组合：

| 场景 | 服装建议 |
|------|----------|
| **白天** | 轻薄 T?恤 或短袖衬衫 + 轻薄长裤/休闲短裤，配上 **薄款风衣/长袖衬衫**（如果你对晚间降温较敏感）。 |
| **晚间/傍晚** | 由于风速不大但温度可能略低，可备一件**薄款针织衫**或**轻柔连帽外套**。 |
| **室内外** | 若室内温度高（如空调房），可穿长袖或加件轻薄外套，方便随时取下。 |
| **鞋子** | 运动鞋/帆布鞋（透气、舒适），如果你要去海边或海风较大，可以选择 **防水/防滑的轻质运动鞋**。 |

**小贴士**
- 太阳直射时记得戴墨镜、涂防晒霜；
- 夜间气温可能比白天低约 5?℃，可随身携带一件轻薄外套，方便调节；
- 如果你计划进行户外运动，建议准备一套**速干、吸汗**的运动服。

总的来说，**轻薄、层叠式穿搭**既能应对上午的温暖，又能在傍晚微凉时保持舒适。祝你今天玩得愉快！
```
