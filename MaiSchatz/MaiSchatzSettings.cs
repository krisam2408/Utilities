using Microsoft.Extensions.Configuration;

namespace MaiSchatz;

public sealed class MaiSchatzSettings
{
    public string Key { get; set; }
    public string Endpoint { get; set; }
    public bool IsEnabled { get; set; }

    public MaiSchatzSettings() { }

    public MaiSchatzSettings(IConfigurationSection configuration)
    {
        string? key = configuration.GetSection("Key").Value;
        string? endpoint = configuration.GetSection("Endpoint").Value;
        string? enabled = configuration.GetSection("Enabled").Value;

        if (string.IsNullOrWhiteSpace(key))
            throw new NullReferenceException(nameof(key));

        if(string.IsNullOrWhiteSpace(endpoint))
            throw new NullReferenceException(nameof(endpoint));

        Key = key;
        Endpoint = endpoint;

        if (string.IsNullOrWhiteSpace(enabled))
        {
            IsEnabled = false;
            return;
        }

        bool handleEnabled()
        {
            if (enabled == "true")
                return true;
            return false;
        }

        IsEnabled = handleEnabled();
    }
}
