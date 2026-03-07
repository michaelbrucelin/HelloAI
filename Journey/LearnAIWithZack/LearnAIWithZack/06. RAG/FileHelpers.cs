using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ude;

namespace LearnAIWithZack._06._RAG
{
    public static class FileHelpers
    {
        public static async Task<string> ReadAllTextAnyEncodingAsync(string filePath,
            CancellationToken cancellationToken = default)
        {
            var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
            // 用Ude检测编码
            var detector = new CharsetDetector();
            detector.Feed(bytes, 0, bytes.Length);
            detector.DataEnd();
            var encoding = Encoding.UTF8;
            if (detector.Charset != null) encoding = Encoding.GetEncoding(detector.Charset);
            return encoding.GetString(bytes);
        }
    }
}
