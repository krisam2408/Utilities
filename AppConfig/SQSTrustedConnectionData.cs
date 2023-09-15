using AppConfig.Abstract;
using static AppConfig.Abstract.IConnectionData;

namespace AppConfig;

public sealed class SQSTrustedConnectionData : IConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.PG;
    public string ConnectionType { get; set; }

    public string Source { get; set; }
    public string Database { get; set; }

    public string ConnectionString => $"Data Source={Source};Initial Catalog={Database};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
}
