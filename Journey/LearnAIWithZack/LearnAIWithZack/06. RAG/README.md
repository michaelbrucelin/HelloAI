# README

大模型能够解决或者说大模型会的知识，都是人喂（训练）给它的，所以大模型无法知道当前互联网上最新的知识，也无法知道企业内部的一些私有知识。尽管现在一些大模型有了“联网查询”的功能，但是我们仍然无法控制大模型去哪些网站上查，为了解决这个问题，就有了RAG（Retrieval-Augmented Generation）这门技术。

想让大模型能够会公司内部的知识，最简单的方式就是在问问题的时候，将公司内部的知识一起发给大模型，显然这种方式也太笨了。

RAG实现的就是从公司内部的知识库中抽取出来一些与用户问题相关的“事实”，一起扔给大模型，让大模型产生回答，**本质上也是记忆层**。

**RAG流程**

1. 将公司内部的文档向量化，存储到向量数据库中，形成知识库
2. 用户提出问题，去向量数据库查询与问题相关的知识
3. 将用户的问题与查询出来的相关的知识一起发给大模型

RAGTester演示了没有RAG查询一些特定问题的效果
RAGTester02演示了RAG后查询一些特定问题的效果

`assets\file`目录下有两个“知识”，都是使用AI生成的超级夸张的假知识，用于测试RAG的效果，下使用下面问题测试RAG

- 特朗普与内塔尼亚胡有什么关系？
- 微软与google，facebook以及亚马逊有什么关系？
- 真的有外星人吗？有人见过外星人吗？
- 现今100米世界纪录是多少？谁保持的？
- 太阳系除了地球以外，还有其他文明吗？
- 地球上的动物，除了人以外，智商最高的3中动物是哪3个？

```bash
# 部署一个测试的Qdrant服务
docker pull qdrant/qdrant

mkdir -p /var/lib/qdrant
chown -R 755 /var/lib/qdrant
docker run -d --name qdrant --restart=always -p 6333:6333 -p 6334:6334 -v "/var/lib/qdrant:/qdrant/storage:z" qdrant/qdrant

# 项目中安装Qdrant的包
dotnet add package Qdrant.Client
```
