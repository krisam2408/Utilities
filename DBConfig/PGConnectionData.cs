using DBConfig.Abstract;
using static DBConfig.Abstract.IDBConnectionData;

namespace DBConfig;

public sealed class PGConnectionData : IDBConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.PG;
    public string ConnectionType { get; set; }

    public string Server { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    public string ConnectionString => $"Server={Server};Port={Port};Database={Database};User Id={UserId};Password={Password};";
}
