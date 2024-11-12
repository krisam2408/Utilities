using System.Text;

namespace Aide;

public static class Byte
{
    public static string TextParse(IEnumerable<byte> chunk)
    {
        byte[] array = chunk.ToArray();
        StringBuilder sb = new();
        int len = array.Length;
        for(int i = 0; i < len; i++)
        {
            int c = i + 1;
            string strByte = array[i]
                .ToString()
                .PadLeft(3, '0');

            sb.Append(strByte);

            if(c.DivisibleBy(16))
            {
                sb.Append('\n');
                continue;
            }

            sb.Append(' ');

            if(c.DivisibleBy(4))
                sb.Append("  ");
        }

        string result = sb.ToString();
        return result;
    }
}
