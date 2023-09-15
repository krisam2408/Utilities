using AppConfig.Abstract;
using static AppConfig.Abstract.IConnectionData;

namespace AppConfig;

public sealed class PGConnectionData : IConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.PG;
    public string ConnectionType { get; set; }

    public string Server { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    public string ConnectionString => $"Server={Server};Port={Port};Database={Database};User Id={UserId};Password={Password};";
}
