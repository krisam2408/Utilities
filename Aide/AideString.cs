using System.Text;

namespace Aide
{
    public static class AideString
    {
        public static string CamelToNormal(this string str, bool keepUpper = true)
        {
            string handleChars(char c, int i)
            {
                if (i == 0)
                    return c.ToString();
                if (char.IsUpper(c))
                    return $" {c}";
                return c.ToString();
            }

            StringBuilder builder = new();
            int len = str.Length;
            for (int i = 0; i < len; i++)
                builder.Append(handleChars(str[i], i));

            if (keepUpper)
                return builder.ToString();

            return builder.ToString().ToLower();
        }
    }
}