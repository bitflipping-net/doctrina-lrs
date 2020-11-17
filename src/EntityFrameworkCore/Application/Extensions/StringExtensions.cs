using System;
using System.Text;

namespace Doctrina.Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToBasicAuth(this string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(bytes);
        }
    }
}