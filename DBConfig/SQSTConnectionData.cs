using DBConfig.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DBConfig;

public sealed class SQSTConnectionData : IDBConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.SQST;
    public string ConnectionType { get; set; }

    public string Source { get; set; }
    public string Database { get; set; }
    public bool Security { get; set; }
    public int Timeout { get; set; } // def: 30
    public bool Encrypt { get; set; }
    public bool TrustCertificate { get; set; }
    public string Intent { get; set; } // def:ReadWrite
    public bool Failover { get; set; }

    public string ConnectionString
    {
        get
        {
            string security = "False";
            if (Security)
                security = "True";

            string encrypt = "False";
            if (Encrypt)
                encrypt = "True";

            string trust = "False";
            if (TrustCertificate)
                trust = "True";

            string failover = "False";
            if (Failover)
                failover = "True";

            return $"Data Source={Source};Initial Catalog={Database};Integrated Security={security};Connect Timeout={Timeout};Encrypt={encrypt};TrustServerCertificate={trust};ApplicationIntent={Intent};MultiSubnetFailover={failover}";
        }
    } 

    public void CreateConnection(DbContextOptionsBuilder options) =>  options.UseSqlServer(ConnectionString);
}
