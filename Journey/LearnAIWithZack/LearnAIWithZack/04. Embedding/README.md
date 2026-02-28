# README

Embedding（嵌入）：text -> vector，即将一段文本转换成一个向量的过程。
将文本转换成向量后，就可以通过数学运算（Conine/Euclid/Dot/Manhattan等）计算两个文本的相似度，至于将文本转成向量的细节，即Embedding的细节就是Embedding模型训练的细节了。

```csharp
double[] v1 = [0.1, 0.3, 0.5, 0.9, 0.7];
double[] v2 = [0.1, 0.2, 0.5, 0.9, 0.7];
double[] v3 = [-0.1, 0.9, -0.3, -0.5, 0.7];

CalculateCosineSimilarity(v1, v2).Dump("(v1, v2): ");  // (v1, v2): 0.9970410769325952
CalculateCosineSimilarity(v1, v3).Dump("(v1, v3): ");  // (v1, v3): 0.0909090909090909
CalculateCosineSimilarity(v2, v3).Dump("(v2, v3): ");  // (v2, v3): 0.036927447293799785
```

这里使用`qwen3-embedding:0.6b`这个模型将10个文本embedding，并存储到List中，然后用户每输入一句话，计算用户输入的embedding，并与List中的“知识”挨个计算相速度。

从例子中可以看到

1. `qwen3-embedding:0.6b`这个模型是同时支持中英文的
   知识都是英文的，输入中文的“什么是机器学习”，搜出来了正确的结果
2. 英文输入错了单词，也是识别的。
   输入“waht is machne learning”，搜出来了正确的结果

