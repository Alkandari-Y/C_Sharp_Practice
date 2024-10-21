using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Helpers
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(string title)
        {
            string slug = title.ToLower();
            slug = slug.Replace(" ", "-");
            slug = RemoveSpecialCharacters(slug);

            return $"{slug}-{DateTime.Now.Ticks}";
        }

        // Need to implement a feature to check blog table for duplicate slugs to ensure uniqueness
        private static string RemoveSpecialCharacters(string text)
        {
            var result = "";
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c) || c == '-')
                {
                    result += c;
                }
            }

            return result;
        }
    }
}