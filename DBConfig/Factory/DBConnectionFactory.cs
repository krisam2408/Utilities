using DBConfig.Abstract;
using Microsoft.Extensions.Configuration;

namespace DBConfig.Factory;

public sealed class DBConnectionFactory
{
    public static IDBConnectionData CreateDBConnectionData(IConfigurationSection configuration)
    {
        string? connectionType = configuration.GetSection("ConnectionType").Value?.ToUpper();

        if (connectionType == ConnectionTypeName.SQS.ToString())
            return CastSQSConnectionData(configuration, connectionType);

        if (connectionType == ConnectionTypeName.SQST.ToString())
            return CastSQSTConnectionData(configuration, connectionType);

        if (connectionType == ConnectionTypeName.PG.ToString())
            return CastPGConnectionData(configuration, connectionType);

        if(connectionType == ConnectionTypeName.MS.ToString())
            return CastMSConnectionData(configuration, connectionType);

        throw new NotImplementedException("There is no casting method for the specified connection type.");
    }

    private static SQSConnectionData CastSQSConnectionData(IConfiguration configuration, string connectionType)
    {
        string? server = configuration.GetSection("Server").Value;
        if (string.IsNullOrWhiteSpace(server))
            throw new NullReferenceException("Server not specified");

        int? port = null;
        string? portValue = configuration.GetSection("Port").Value;
        if (!string.IsNullOrWhiteSpace(portValue))
        {
            if(int.TryParse(portValue, out int p)) 
                port = p;
        }

        string? database = configuration.GetSection("Database").Value;

        if (string.IsNullOrWhiteSpace(database))
            throw new NullReferenceException("Database not specified");

        string? userId = configuration.GetSection("UserId").Value;

        if (string.IsNullOrWhiteSpace(userId))
            throw new NullReferenceException("User id not specified");

        string? password = configuration.GetSection("Password").Value;

        if (string.IsNullOrWhiteSpace(password))
            throw new NullReferenceException("Password is not specified");

        string? securityValue = configuration.GetSection("Security").Value;
        bool security = securityValue?.ToLower() == "true";

        int timeout = 30;
        string? timeoutValue = configuration.GetSection("Timeout").Value;
        if(int.TryParse(timeoutValue, out int t))
            timeout = t;

        string? encryptValue = configuration.GetSection("Encrypt").Value;
        bool encrypt = encryptValue?.ToLower() == "true";

        string? trustValue = configuration.GetSection("TrustCertificate").Value;
        bool trust = trustValue?.ToLower() == "true";

        string intent = "ReadWrite";
        string? intentValue = configuration.GetSection("Intent").Value;
        if (intentValue == "ReadOnly")
            intent = intentValue;

        string? failoverValue = configuration.GetSection("Failover").Value;
        bool failover = failoverValue?.ToLower() == "true";

        return new SQSConnectionData()
        {
            ConnectionType = connectionType,
            Server = server,
            Port = port,
            Database = database,
            UserId = userId,
            Password = password,
            Security = security,
            Timeout = timeout,
            Encrypt = encrypt,
            TrustCertificate = trust,
            Intent = intent,
            Failover = failover
        };
    }

    private static SQSTConnectionData CastSQSTConnectionData(IConfigurationSection configuration, string connectionType)
    {
        string? source = configuration.GetSection("Source").Value;

        if (string.IsNullOrWhiteSpace(source))
            throw new NullReferenceException("Source not specified");

        string? database = configuration.GetSection("Database").Value;

        if (string.IsNullOrWhiteSpace(database))
            throw new NullReferenceException("Database not specified");

        string? securityValue = configuration.GetSection("Security").Value;
        bool security = securityValue?.ToLower() == "true";

        int timeout = 30;
        string? timeoutValue = configuration.GetSection("Timeout").Value;
        if (int.TryParse(timeoutValue, out int t))
            timeout = t;

        string? encryptValue = configuration.GetSection("Encrypt").Value;
        bool encrypt = encryptValue?.ToLower() == "true";

        string? trustValue = configuration.GetSection("TrustCertificate").Value;
        bool trust = trustValue?.ToLower() == "true";

        string intent = "ReadWrite";
        string? intentValue = configuration.GetSection("Intent").Value;
        if (intentValue == "ReadOnly")
            intent = intentValue;

        string? failoverValue = configuration.GetSection("Failover").Value;
        bool failover = failoverValue?.ToLower() == "true";

        return new SQSTConnectionData()
        {
            ConnectionType = connectionType,
            Source = source,
            Database = database,
            Security = security,
            Timeout = timeout,
            Encrypt = encrypt,
            TrustCertificate = trust,
            Intent = intent,
            Failover = failover,
        };
    }

    private static PGConnectionData CastPGConnectionData(IConfigurationSection configuration, string connectionType)
    {
        string? server = configuration.GetSection("Server").Value;

        if (string.IsNullOrWhiteSpace(server))
            throw new NullReferenceException("Server not specified");

        string? portValue = configuration.GetSection("Port").Value;

        if (string.IsNullOrWhiteSpace(portValue))
            throw new NullReferenceException("Port not specified");

        if (!int.TryParse(portValue, out int port))
            throw new FormatException("Port is not in the right format");

        string? database = configuration.GetSection("Database").Value;

        if (string.IsNullOrWhiteSpace(database))
            throw new NullReferenceException("Database not specified");

        string? userId = configuration.GetSection("UserId").Value;

        if (string.IsNullOrWhiteSpace(userId))
            throw new NullReferenceException("User id not specified");

        string? password = configuration.GetSection("Password").Value;

        if (string.IsNullOrWhiteSpace(password))
            throw new NullReferenceException("Password is not specified");

        return new PGConnectionData()
        {
            ConnectionType = connectionType,
            Server = server,
            Port = port,
            Database = database,
            UserId = userId,
            Password = password
        };
    }

    private static MSConnectionData CastMSConnectionData(IConfigurationSection configuration, string connectionType)
    {
        string? server = configuration.GetSection("Server").Value;

        if (string.IsNullOrWhiteSpace(server))
            throw new NullReferenceException("Server not specified");

        int? port = null;
        string? portValue = configuration.GetSection("Port").Value;

        if (!string.IsNullOrWhiteSpace(portValue))
        {
            if(int.TryParse(portValue, out int p))
                port = p;
        }

        string? database = configuration.GetSection("Database").Value;

        if (string.IsNullOrWhiteSpace(database))
            throw new NullReferenceException("Database not specified");

        string? user = configuration.GetSection("User").Value;

        if (string.IsNullOrWhiteSpace(user))
            throw new NullReferenceException("User not specified");

        string? password = configuration.GetSection("Password").Value;

        if (string.IsNullOrWhiteSpace(password))
            throw new NullReferenceException("Password is not specified");
        
        string? version = configuration.GetSection("Version").Value;

        if (string.IsNullOrWhiteSpace(version))
            throw new NullReferenceException("Engine version is not specified");

        return new MSConnectionData()
        {
            Server = server,
            Port = port,
            Database = database,
            User = user,
            Password = password,
            Version = version
        };
    }
}
