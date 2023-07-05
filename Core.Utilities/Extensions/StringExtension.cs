using Core.Utilities.Ensures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class StringExtension
    {
        public static byte[] Encoding_UTF8(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
        public static bool IsBase64String(this string value)
        {
       
            Span<byte> bytes = stackalloc byte[(int)(value.Length * 0.75)];
            return Convert.TryFromBase64String(value, bytes, out int bytesWritten);
        }

        public static string ToCamelCase(this string value)
        {
            var words = value.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
            var leadWord = Regex.Replace(words[0], @"([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\w*)",
                m =>
                {
                    return m.Groups[1].Value.ToLower() + m.Groups[2].Value.ToLower() + m.Groups[3].Value;
                });
            var tailWords = words.Skip(1)
                .Select(word => char.ToUpper(word[0]) + word[1..])
                .ToArray();
            return $"{leadWord}{string.Join(string.Empty, tailWords)}";
        }
        public static string ToPascalCase(this string value)
        {
            var words = value.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
            var pascalWords = words.Select(word => char.ToUpper(word[0]) + word[1..].ToLower());
            return string.Join(string.Empty, pascalWords);
        }

       
    }
}
