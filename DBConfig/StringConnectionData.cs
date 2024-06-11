using DBConfig.Abstract;
using static DBConfig.Abstract.IDBConnectionData;

namespace DBConfig;

public sealed class StringConnectionData : IDBConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.ConnectionString;
    public string ConnectionType { get; set; }
    public string Connection { get; set; }

    public string ConnectionString 
    {
        get
        {
            return Connection;
        }
    }

}
