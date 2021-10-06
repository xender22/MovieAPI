using MovieAPI.Data.Models;

namespace MovieAPI.Extensions
{
    public static class Extensions
    {
        private static bool HasValue(this string value) => !string.IsNullOrEmpty(value);

        public static bool IsValid(this Metadata m)
        {
            return m.Title.HasValue() &&
                   m.Language.HasValue() &&
                   m.Duration.HasValue();
        }

    }
}
