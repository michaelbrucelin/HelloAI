# [.NET + AI 生态系统工具和 SDK](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem)

## 本文内容

1. [Microsoft.Extensions.AI 库](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#microsoftextensionsai-libraries)
2. [其他与 AI 相关的 Microsoft.Extensions 库](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#other-ai-related-microsoftextensions-libraries)
3. [Microsoft代理框架](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#microsoft-agent-framework)
4. [.NET 的语义内核](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#semantic-kernel-for-net)
5. [用于生成 AI 应用的 .NET SDK](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#net-sdks-for-building-ai-apps)
6. [使用本地 AI 模型开发](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#develop-with-local-ai-models)
7. [后续步骤](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#next-steps)

显示另外 3 个

.NET 生态系统提供了许多强大的工具、库和服务来开发 AI 应用程序。 .NET 支持云和本地 AI 模型连接、适用于各种 AI 和矢量数据库服务的许多不同的 SDK 以及其他工具，有助于构建范围和复杂性不同的智能应用。

重要

本文中介绍的所有 SDK 和服务并非都由Microsoft维护。 在考虑 SDK 时，请务必评估其质量、许可、支持和兼容性，以确保它们满足你的要求。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#microsoftextensionsai-libraries)

## Microsoft.Extensions.AI 库

[`Microsoft.Extensions.AI`](https://learn.microsoft.com/zh-cn/dotnet/ai/microsoft-extensions-ai) 是一组核心 .NET 库，提供统一的 C# 抽象层，用于与 AI 服务交互，例如小型和大型语言模型（SLM 和 LLM）、嵌入和中间件。 这些 API 是在与 .NET 生态系统中的开发人员协作创建的。 低级别 API（例如 [IChatClient](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.ichatclient) ，和 [IEmbeddingGenerator<TInput,TEmbedding>](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.iembeddinggenerator-2)）是从语义内核中提取的，并已移动到 [Microsoft.Extensions.AI](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai) 命名空间中。

`Microsoft.Extensions.AI` 提供可由各种服务实现的抽象，所有这些概念都遵循相同的核心概念。 此库不旨在提供针对任何特定提供商服务定制的 API。 `Microsoft.Extensions.AI`目标是在 .NET 生态系统中充当一个统一层，使开发人员能够选择他们的首选框架和库，同时确保整个生态系统之间的无缝集成和协作。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#other-ai-related-microsoftextensions-libraries)

## 其他与 AI 相关的 Microsoft.Extensions 库

[📦 Microsoft.Extensions.VectorData.Abstractions 包](https://www.nuget.org/packages/Microsoft.Extensions.VectorData.Abstractions/)提供了一个统一的抽象层，用于与各种矢量存储进行交互。 它允许在矢量存储（如 Qdrant、Azure SQL、CosmosDB、MongoDB、ElasticSearch 等）中存储已处理的区块。 有关详细信息，请参阅 [生成 .NET AI 矢量搜索应用](https://learn.microsoft.com/zh-cn/dotnet/ai/vector-stores/how-to/build-vector-search-app)。

[📦 Microsoft.Extensions.DataIngestion 包](https://www.nuget.org/packages/Microsoft.Extensions.DataIngestion)提供用于数据引入的基础 .NET 构建基块。 它使开发人员能够读取、处理和准备 AI 和机器学习工作流的文档，特别适用于检索增强生成（RAG）场景。 有关详细信息，请参阅 [数据引入](https://learn.microsoft.com/zh-cn/dotnet/ai/conceptual/data-ingestion)。

[](https://learn.microsoft.com/zh-cn/dotnet/ai/dotnet-ai-ecosystem#microsoft-agent-framework)

## Microsoft代理框架

如果想要使用低级别服务，例如 [IChatClient](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.ichatclient) 和 [IEmbeddingGenerator<TInput,TEmbedding>](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.iembeddinggenerator-2)，你可以直接从应用中引用 `Microsoft.Extensions.AI.Abstractions` 包。 但是，如果要生成具有更高级别的业务流程功能的代理 AI 应用程序，则应使用 [Microsoft Agent Framework](https://learn.microsoft.com/zh-cn/agent-framework/overview/agent-framework-overview)。 代理框架基于 `Microsoft.Extensions.AI.Abstractions` 包构建，并为不同服务（如 OpenAI、Azure OpenAI、Microsoft Foundry 等）提供具体实现 [IChatClient](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.ai.ichatclient)。

对于需要通过高级业务流程、多代理协作以及企业级安全性和可观测性构建代理 AI 系统的 .NET 应用，建议使用此框架。

Agent Framework 是一个生产就绪的开源框架，它汇集了语义内核和 Microsoft Research 的 AutoGen 的最佳功能。 代理框架提供：

- **多代理业务流程**：支持顺序、并发、群聊、交接和_磁性_（其中主代理指导其他代理）的业务流程模式。
- **云和提供商的灵活性**：基于插件和连接器模型，实现云无关（如容器、本地或多云架构）和提供商无关（例如，OpenAI 或 Foundry）。
- **企业级功能**：内置可观测性（OpenTelemetry）、Microsoft Entra 安全集成，以及负责任的 AI 功能，包括提示注入保护和任务遵守监视。
- **基于标准的互作性**：与开放标准（如代理到代理（A2A）协议和模型上下文协议（MCP）集成，以便进行代理发现和工具交互。

有关详细信息，请参阅 [Microsoft Agent Framework 文档](https://learn.microsoft.com/zh-cn/agent-framework/overview/agent-framework-overview)。
