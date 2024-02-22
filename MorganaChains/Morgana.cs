using System.Security.Cryptography;
using System.Text;

namespace MorganaChains;

public sealed class Morgana
{
    public byte[] SecretKey { get; private set; }
    public byte[] PublicKey { get; private set; }

    private static Morgana m_instance;

    public static Morgana Instance
    {
        get
        {
            if (m_instance == null)
                throw new NullReferenceException();
            return m_instance;
        }
    }

    public static void Initialize(ParallelSettings settings)
    {
        m_instance = new Morgana(settings);
    }

    private Morgana(ParallelSettings settings)
    {
        SecretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
        PublicKey = Encoding.UTF8.GetBytes(settings.PublicKey);
    }

    public string Encrypt(string text)
    {
        byte[] textByteArray = Encoding.UTF8.GetBytes(text);

        using (Aes aes = Aes.Create())
        using (MemoryStream ms = new())
        using (CryptoStream cs = new(ms, aes.CreateEncryptor(PublicKey, SecretKey), CryptoStreamMode.Write))
        {
            cs.Write(textByteArray, 0, textByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
    }

    public string Decrypt(string text)
    {
        text = text.Replace(" ", "+");
        byte[] textByteArray = Convert.FromBase64String(text);

        using (Aes aes = Aes.Create())
        using (MemoryStream ms = new())
        using (CryptoStream cs = new(ms, aes.CreateDecryptor(PublicKey, SecretKey), CryptoStreamMode.Write))
        {
            cs.Write(textByteArray, 0, textByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }

    public static string GeneratePassword()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new();
        int len = random.Next(5, 10);

        StringBuilder sb = new();

        for (int i = 0; i < len; i++)
            sb.Append(chars[random.Next(chars.Length)]);

        string finalString = sb.ToString();

        return finalString;
    }
}
