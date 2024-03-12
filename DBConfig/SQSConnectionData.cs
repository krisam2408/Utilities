using DBConfig.Abstract;
using static DBConfig.Abstract.IDBConnectionData;

namespace DBConfig;

public sealed class SQSConnectionData : IDBConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.SQS;
    public string ConnectionType { get; set; }

    public string Server { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    public string ConnectionString
    {
        get
        {
            string port = "";
            if(!string.IsNullOrWhiteSpace(Port))
                port = $",{Port}";

            return $"Data Source={Server}{port};Initial Catalog={Database};User Id={UserId};Password={Password};Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
    } 
