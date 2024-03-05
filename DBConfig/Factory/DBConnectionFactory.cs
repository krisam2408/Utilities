using AppConfig.Abstract;
using Microsoft.Extensions.Configuration;
using static AppConfig.Abstract.IDBConnectionData;

namespace AppConfig.Factory;

public sealed class DBConnectionFactory
{
    public static IDBConnectionData CreateDBConnectionData(IConfigurationSection configuration)
    {
        string? connectionType = configuration.GetSection("ConnectionType").Value;

        if (connectionType == ConnectionTypeName.SQS.ToString())
            return CastSQSConnectionData(configuration, connectionType);

        if (connectionType == ConnectionTypeName.SQSTrusted.ToString())
            return CastSQSTrustedConnection(configuration, connectionType);

        if (connectionType == ConnectionTypeName.PG.ToString())
            return CastPGConnection(configuration, connectionType);

        throw new NotImplementedException("There is no casting method for the specified connection type.");
    }

    private static SQSConnectionData CastSQSConnectionData(IConfiguration configuration, string connectionType)
    {
        string? server = configuration.GetSection("Server").Value;

        if (string.IsNullOrWhiteSpace(server))
            throw new NullReferenceException("Server not specified");

        string? database = configuration.GetSection("Database").Value;

        if (string.IsNullOrWhiteSpace(database))
            throw new NullReferenceException("Database not specified");

        string? userId = configuration.GetSection("UserId").Value;

        if (string.IsNullOrWhiteSpace(userId))
            throw new NullReferenceException("User id not specified");

        string? password = configuration.GetSection("Password").Value;

        if (string.IsNullOrWhiteSpace(password))
            throw new NullReferenceException("Password is not specified");

        return new SQSConnectionData()
        {
            ConnectionType = connectionType,
            Server = server,
            Database = database,
            UserId = userId,
            Password = password
        };
    }

    private static SQSTrustedConnectionData CastSQSTrustedConnection(IConfigurationSection configuration, string connectionType)
    {
        string? source = configuration.GetSection("Source").Value;

        if (string.IsNullOrWhiteSpace(source))
            throw new NullReferenceException("Source not specified");

        string? database = configuration.GetSection("Database").Value;

        if (string.IsNullOrWhiteSpace(database))
            throw new NullReferenceException("Database not specified");

        return new SQSTrustedConnectionData()
        {
            ConnectionType = connectionType,
            Source = source,
            Database = database
        };
    }

    private static PGConnectionData CastPGConnection(IConfigurationSection configuration, string connectionType)
    {
        string? server = configuration.GetSection("Server").Value;

        if (string.IsNullOrWhiteSpace(server))
            throw new NullReferenceException("Server not specified");

        string? portStr = configuration.GetSection("Port").Value;

        if (string.IsNullOrWhiteSpace(portStr))
            throw new NullReferenceException("Port not specified");

        if (int.TryParse(portStr, out int port))
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
}
