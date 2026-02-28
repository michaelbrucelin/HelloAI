# README

前面演示了记忆层与embedding，这里有两个问题

1. 每次都把“知识”embedding，既浪费资源，又浪费时间
2. 每次都把输入和所有知识做一次相似度计算，耗时

所以生产中都把计算好的embedding存储在向量数据库中，这样既不用重复的embedding，也可以高效的查找（索引？不清楚技术细节）。

```bash
# 部署一个测试的Qdrant服务
docker pull qdrant/qdrant

mkdir -p /var/lib/qdrant
chown -R 755 /var/lib/qdrant
docker run -d --name qdrant --restart=always -p 6333:6333 -p 6334:6334 -v "/var/lib/qdrant:/qdrant/storage:z" qdrant/qdrant

# 项目中安装Qdrant的包
dotnet add package Qdrant.Client
```
