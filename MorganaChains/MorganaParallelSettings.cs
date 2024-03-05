using Microsoft.Extensions.Configuration;

namespace MorganaChains;

public sealed class MorganaParallelSettings
{
    public string PublicKey { get; private set; }
    public string SecretKey { get; private set; }

    public MorganaParallelSettings() { }

    public MorganaParallelSettings(IConfigurationSection configuration)
    {
        string? secretKey = configuration.GetSection("SecretKey").Value;
        string? publicKey = configuration.GetSection("PublicKey").Value;

        if(string.IsNullOrWhiteSpace(secretKey))
            throw new NullReferenceException(nameof(secretKey));

        if(string.IsNullOrWhiteSpace(publicKey))
            throw new NullReferenceException(nameof(publicKey));

        PublicKey = publicKey;
        SecretKey = secretKey;
    }
}
