using Doctrina.ExperienceApi.Data.Helpers;

namespace Doctrina.Application.Infrastructure
{
    public static class StringExtensions
    {
        public static string ComputeHash(this string s)
        {
            return SHAHelper.SHA1.ComputeHash(s);
        }
    }
}