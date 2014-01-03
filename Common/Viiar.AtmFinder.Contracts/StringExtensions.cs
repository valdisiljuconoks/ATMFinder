using System.Linq;
using System.Text;

namespace Viiar.AtmFinder.Contracts
{
    public static class StringExtensions
    {
        public static string TakeFirst(this string value, int count)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            var sb = new StringBuilder();
            sb.Append(value.ToCharArray().Take(count).ToArray());
            sb.Append(value.Length > count ? "..." : string.Empty);

            return sb.ToString();
        }
    }
}
