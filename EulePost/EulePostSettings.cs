using Microsoft.Extensions.Configuration;

namespace EulePost;

public sealed class EulePostSettings
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }

    public EulePostSettings() { }

    public EulePostSettings(IConfigurationSection configuration)
    {
        string? email = configuration.GetSection("Address").Value;
        string? password = configuration.GetSection("Password").Value;
        string? host = configuration.GetSection("Host").Value;
        string? port = configuration.GetSection("Port").Value;

        if(string.IsNullOrWhiteSpace(email))
            throw new NullReferenceException(nameof(email));

        if(string.IsNullOrWhiteSpace(password))
            throw new NullReferenceException(nameof(password));

        if(string.IsNullOrWhiteSpace(host))
            throw new NullReferenceException(nameof(host));

        if(int.TryParse(port, out int portNum))
        {
            EmailAddress = email;
            Password = password;
            Host = host;
            Port = portNum;
            return;
        }

        throw new NullReferenceException(nameof(port));
    }
}
