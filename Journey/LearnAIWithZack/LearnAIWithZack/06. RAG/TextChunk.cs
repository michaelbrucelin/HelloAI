using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearnAIWithZack._06._RAG
{
    public record TextChunk(
       [property: JsonPropertyName("chunk_id")] int ChunkId,
       [property: JsonPropertyName("content")] string Content,
       [property: JsonPropertyName("summary_title")] string SummaryTitle,
       [property: JsonPropertyName("keywords")] List<string> Keywords,
       [property: JsonPropertyName("char_count")] int CharCount
   );
}
