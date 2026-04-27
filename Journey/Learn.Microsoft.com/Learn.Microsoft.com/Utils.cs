using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Microsoft.com
{
    public static class Utils
    {
        public static string base_url = "http://192.168.1.211:11434";
        public static string model_deepseek_8b = "deepseek-r1:8b";
        public static string model_deepseek_32b = "deepseek-r1:32b";
        public static string model_chatgpt_20b = "gpt-oss:20b";

        public static void WriteSplitLine(int id, char c = '=', int cnt = 64)
        {
            Console.WriteLine($"{Environment.NewLine}{new string('=', 64)} {id}. {new string(c, cnt)}{Environment.NewLine}");
        }
    }
}
