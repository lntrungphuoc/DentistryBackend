using System.Text;
using System.Text.RegularExpressions;

namespace AppDentistry.Common.Providers
{
    public static partial class StringUtils
    {
        public static string ToUnSign(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string GenerateUrlTitle(string title)
        {
            title = $"{Regex.Replace(title.ToUnSign().ToLower(), "[^a-z0-9]+", "-")}-{DateTime.Now.Ticks}";
            title = title.Replace("--", "-");
            return title;
        }
    }
}
