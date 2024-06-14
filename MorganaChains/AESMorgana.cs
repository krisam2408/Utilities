using System.Security.Cryptography;
using System.Text;

namespace MorganaChains;

public sealed class AESMorgana
{
    public readonly byte[] m_secretKey;
    public readonly byte[] m_publicKey;

    public AESMorgana(AESMorganaSettings settings)
    {
        m_secretKey = settings.SecretKey;
        m_publicKey = settings.PublicKey;
    }

    public string Encrypt(string text)
    {
        byte[] textByteArray = Encoding.UTF8.GetBytes(text);

        using (Aes aes = Aes.Create())
        using (MemoryStream ms = new())
        using (CryptoStream cs = new(ms, aes.CreateEncryptor(m_publicKey, m_secretKey), CryptoStreamMode.Write))
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
        using (CryptoStream cs = new(ms, aes.CreateDecryptor(m_publicKey, m_secretKey), CryptoStreamMode.Write))
        {
            cs.Write(textByteArray, 0, textByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
