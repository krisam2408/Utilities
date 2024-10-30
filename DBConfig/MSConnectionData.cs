using DBConfig.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DBConfig;

public sealed class MSConnectionData : IDBConnectionData
{
    public const ConnectionTypeName TypeName = ConnectionTypeName.MS;
    public string ConnectionType { get; set; }

    public string Server { get; set; }
    public int? Port { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Version { get; set; }

    public string ConnectionString
    {
        get
        {
            string port = "";
            if(Port is not null)
                port = $":{Port}";
            return $"server={Server}{port};user={User};password={Password};database={Database}";
        }
    }

    public void CreateConnection(DbContextOptionsBuilder options)
    {
        int[] version = Version
            .Split('.')
            .Select(i => int.Parse(i))
            .ToArray();

        MySqlServerVersion mySqlVersion = new(new Version(version[0], version[1], version[2]));

        options.UseMySql(ConnectionString, mySqlVersion);
    }
}
