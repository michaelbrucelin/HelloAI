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
