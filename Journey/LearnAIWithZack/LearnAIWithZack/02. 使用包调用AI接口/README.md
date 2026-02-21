# README

使用 Microsoft.Extensions.AI.OpenAI 这个nuget包来调用AI接口，没找到 Microsoft.Extensions.AI.Ollama 包，所以使用 Microsoft.Extensions.AI.OpenAI。

```powershell
dotnet add package Microsoft.Extensions.AI.OpenAI
```

安装 HttpMataki.NET.Auto 包，这个包安装完成后，只需要在代码中引入 `HttpClientAutoInterceptor.StartInterception();` 即可实现看到项目中 http 请求和响应的全部过程，将截取的结果输出到控制台，有点 tcpdump 的意思。
