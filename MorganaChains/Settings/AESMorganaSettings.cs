using Microsoft.Extensions.Configuration;
using System.Text;

namespace MorganaChains;

public sealed class AESMorganaSettings
{
    public byte[] PublicKey { get; private set; }
    public byte[] SecretKey { get; private set; }

    public AESMorganaSettings() { }

    public AESMorganaSettings(IConfigurationSection configuration)
    {
        string? publicKey = configuration.GetSection("PublicKey").Value;
        string? secretKey = configuration.GetSection("SecretKey").Value;
        
        if (string.IsNullOrWhiteSpace(publicKey))
            throw new NullReferenceException(nameof(publicKey));

        if (string.IsNullOrWhiteSpace(secretKey))
            throw new NullReferenceException(nameof(secretKey));

        PublicKey = Encode(publicKey);
        SecretKey = Encode(secretKey);
    }

    public AESMorganaSettings(string publicKey, string secretKey)
    {
        PublicKey = Encode(publicKey); 
        SecretKey = Encode(secretKey);
    }

    private static byte[] Encode(string key)
    {
        return Encoding.UTF8.GetBytes(key);
    }
}
