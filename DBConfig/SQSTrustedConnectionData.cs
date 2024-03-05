using DBConfig.Abstract;
using static DBConfig.Abstract.IDBConnectionData;

namespace DBConfig;

public sealed class SQSTrustedConnectionData : IDBConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.SQSTrusted;
    public string ConnectionType { get; set; }

    public string Source { get; set; }
    public string Database { get; set; }

    public string ConnectionString => $"Data Source={Source};Initial Catalog={Database};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
}
