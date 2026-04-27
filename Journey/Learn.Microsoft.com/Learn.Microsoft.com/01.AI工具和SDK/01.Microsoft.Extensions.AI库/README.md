# [Microsoft.Extensions.AI 库](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai)

## 本文内容

1. [包](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-packages)
2. [API 和功能](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#apis-and-functionality)
3. [使用 Microsoft.Extensions.AI 进行构建](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#build-with-microsoftextensionsai)
4. [另请参阅](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#see-also)

.NET 开发人员需要在其应用中集成和与越来越多的人工智能（AI）服务进行交互。 这些 `Microsoft.Extensions.AI` 库提供了一种统一的方法来表示生成 AI 组件，并实现与各种 AI 服务的无缝集成和互作性。 本文介绍这些库，并提供深入的用法示例来帮助你入门。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-packages)

## 包

[📦 Microsoft.Extensions.AI.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.AI.Abstractions) 包提供核心交换类型，包括[IChatClient](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.ichatclient)和 [IEmbeddingGenerator<TInput,TEmbedding>](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.iembeddinggenerator-2)。 提供 LLM 客户端的任何 .NET 库都可以实现 `IChatClient` 接口，以实现与使用代码的无缝集成。

[📦 Microsoft.Extensions.AI](https://www.nuget.org/packages/Microsoft.Extensions.AI) 包对 `Microsoft.Extensions.AI.Abstractions` 包具有隐式依赖项。 通过此包，可以使用熟悉的依赖项注入和中间件模式轻松地将自动函数工具调用、遥测和缓存等组件集成到应用程序中。 例如，它提供了 [UseOpenTelemetry(ChatClientBuilder, ILoggerFactory, String, Action<OpenTelemetryChatClient>)](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.opentelemetrychatclientbuilderextensions.useopentelemetry#microsoft-extensions-ai-opentelemetrychatclientbuilderextensions-useopentelemetry\(microsoft-extensions-ai-chatclientbuilder-microsoft-extensions-logging-iloggerfactory-system-string-system-action\(\(microsoft-extensions-ai-opentelemetrychatclient\)\)\)) 扩展方法，该方法将 OpenTelemetry 支持添加到聊天客户端管道中。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#which-package-to-reference)

### 要引用的包

要使用用于生成式 AI 模块的更高级实用工具，请参考 `Microsoft.Extensions.AI` 包（它本身引用 `Microsoft.Extensions.AI.Abstractions`）。 大多数消耗的应用程序和服务应引用 `Microsoft.Extensions.AI` 包以及一个或多个库，这些库提供抽象的具体实现。

提供抽象实现的库通常仅引用`Microsoft.Extensions.AI.Abstractions`。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#install-the-packages)

### 安装软件包

有关如何安装 NuGet 包的信息，请参阅 .NET 应用程序中 [的 dotnet 包添加](https://learn.microsoft.com/zh-cn/dotnet/core/tools/dotnet-package-add) 或 [管理包依赖项](https://learn.microsoft.com/zh-cn/dotnet/core/tools/dependencies)。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#apis-and-functionality)

## API 和功能

- [接口`IChatClient`](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-ichatclient-interface)
- [接口`IEmbeddingGenerator`](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-iembeddinggenerator-interface)
- [接口 `IImageGenerator` （实验性）](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-iimagegenerator-interface-experimental)

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-ichatclient-interface)

### `IChatClient` 接口

[IChatClient](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.ichatclient) 接口定义一个客户端抽象，负责与提供聊天功能的 AI 服务交互。 它包括发送和接收具有多模式内容（如文本、图像和音频）的消息的方法，无论是作为一个完整的集合还是增量流式传输。

有关详细信息和详细的用法示例，请参阅 [“使用 IChatClient 接口](https://learn.microsoft.com/zh-cn/dotnet/ai/ichatclient)”。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-iembeddinggenerator-interface)

### `IEmbeddingGenerator` 接口

[IEmbeddingGenerator](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.iembeddinggenerator) 接口表示嵌入的泛型生成器。 对于泛型类型参数， `TInput` 是嵌入的输入值的类型，是 `TEmbedding` 生成的嵌入类型，它继承自 [Embedding](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.embedding) 类。

有关详细信息和详细的用法示例，请参阅 [使用 IEmbeddingGenerator 接口](https://learn.microsoft.com/zh-cn/dotnet/ai/iembeddinggenerator)。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai#the-iimagegenerator-interface-experimental)

### IImageGenerator 接口（实验性）

该 [IImageGenerator](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.iimagegenerator) 接口表示用于从文本提示或其他输入创建图像的生成器。 此接口使应用程序能够通过一致的 API 集成来自各种 AI 服务的图像生成功能。 该接口支持文本到图像生成（通过调用 [GenerateAsync(ImageGenerationRequest, ImageGenerationOptions, CancellationToken)](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.iimagegenerator.generateasync#microsoft-extensions-ai-iimagegenerator-generateasync\(microsoft-extensions-ai-imagegenerationrequest-microsoft-extensions-ai-imagegenerationoptions-system-threading-cancellationtoken\))）和图像大小和格式 [的配置选项](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.imagegenerationoptions) 。 与库中的其他接口一样，它可以与中间件组成，用于缓存、遥测和其他交叉问题。
