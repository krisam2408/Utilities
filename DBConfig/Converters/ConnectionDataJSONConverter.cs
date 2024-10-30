using DBConfig.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DBConfig.Converters;

public sealed class ConnectionDataJSONConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) =>objectType == typeof(IDBConnectionData);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);

        string? connectionType = (string?)obj["ConnectionType"];

        if (string.IsNullOrWhiteSpace(connectionType))
            throw new JsonException("Connection type not specified");

        if (connectionType == ConnectionTypeName.SQS.ToString())
            return CastSQSConnection(obj, connectionType);

        if (connectionType == ConnectionTypeName.SQST.ToString())
            return CastSQSTConnection(obj, connectionType);

        if (connectionType == ConnectionTypeName.PG.ToString())
            return CastPGConnection(obj, connectionType);

        if(connectionType == ConnectionTypeName.MS.ToString())
            return CastMSConnection(obj, connectionType);

        throw new JsonException("A valid deserialization type not found");
    }

    private static SQSConnectionData CastSQSConnection(JObject obj, string connectionType)
    {
        string? server = (string?)obj["Server"];

        if (string.IsNullOrWhiteSpace(server))
            throw new JsonException("Server not specified");

        int? port = (int?)obj["Port"];
        
        string? database = (string?)obj["Database"];

        if (string.IsNullOrWhiteSpace(database))
            throw new JsonException("Database not specified");

        string? userId = (string?)obj["UserId"];

        if (string.IsNullOrWhiteSpace(userId))
            throw new JsonException("User id not specified");

        string? password = (string?)obj["Password"];

        if (string.IsNullOrWhiteSpace(password))
            throw new JsonException("Password not specified");

        bool? security = (bool?)obj["Security"];
        if (security is null)
            security = false;

        int? timeout = (int?)obj["Timeout"];
        if (timeout is null)
            timeout = 30;

        bool? encrypt = (bool?)obj["Encrypt"];
        if (encrypt is null)
            encrypt = false;

        bool? trust = (bool?)obj["TrustCertificate"];
        if (trust is null)
            trust = false;

        string[] validIntents = ["ReadWrite", "ReadOnly"];
        string? intent = (string?)obj["Intent"];
        if (!validIntents.Contains(intent))
            intent = validIntents[0];

        bool? failover = (bool?)obj["Failover"];
        if (failover is null)
            failover = false;

        return new SQSConnectionData()
        {
            ConnectionType = connectionType,
            Server = server,
            Port = port,
            Database = database,
            UserId = userId,
            Password = password,
            Security = security.Value,
            Timeout = timeout.Value,
            Encrypt = encrypt.Value,
            TrustCertificate = trust.Value,
            Intent = intent,
            Failover = failover.Value,
        };
    }

    private static SQSTConnectionData CastSQSTConnection(JObject obj, string connectionType)
    {
        string? source = (string?)obj["Source"];

        if (string.IsNullOrWhiteSpace(source))
            throw new JsonException("Source not specified");

        string? database = (string?)obj["Database"];

        if (string.IsNullOrWhiteSpace(database))
            throw new JsonException("Database not specified");

        bool? security = (bool?)obj["Security"];
        if (security is null)
            security = false;

        int? timeout = (int?)obj["Timeout"];
        if (timeout is null)
            timeout = 30;

        bool? encrypt = (bool?)obj["Encrypt"];
        if (encrypt is null)
            encrypt = false;

        bool? trust = (bool?)obj["TrustCertificate"];
        if (trust is null)
            trust = false;

        string[] validIntents = ["ReadWrite", "ReadOnly"];
        string? intent = (string?)obj["Intent"];
        if (!validIntents.Contains(intent))
            intent = validIntents[0];

        bool? failover = (bool?)obj["Failover"];
        if (failover is null)
            failover = false;


        return new SQSTConnectionData()
        {
            ConnectionType = connectionType,
            Source = source,
            Database = database,
            Security = security.Value,
            Timeout = timeout.Value,
            Encrypt = encrypt.Value,
            TrustCertificate = trust.Value,
            Intent = intent,
            Failover = failover.Value,
        };
    }

    private static PGConnectionData CastPGConnection(JObject obj, string connectionType)
    {
        string? server = (string?)obj["Server"];

        if (string.IsNullOrWhiteSpace(server))
            throw new JsonException("Server not specified");

        int? port = (int?)obj["Port"];
        if (port is null)
            throw new JsonException("Port not specified");

        string? database = (string?)obj["Database"];

        if (string.IsNullOrWhiteSpace(database))
            throw new JsonException("Database not specified");

        string? userId = (string?)obj["UserId"];

        if (string.IsNullOrWhiteSpace(userId))
            throw new JsonException("User id not specified");

        string? password = (string?)obj["Password"];

        if (string.IsNullOrWhiteSpace(password))
            throw new JsonException("Password not specified");

        return new PGConnectionData()
        {
            ConnectionType = connectionType,
            Server = server,
            Port = port.Value,
            Database = database,
            UserId = userId,
            Password = password
        };
    }

    private static MSConnectionData CastMSConnection(JObject obj, string connectionType)
    {
        string? server = (string?)obj["Server"];

        if (string.IsNullOrWhiteSpace(server))
            throw new JsonException("Server not specified");

        int? port = (int?)obj["Port"];

        string? database = (string?)obj["Database"];

        if (string.IsNullOrWhiteSpace(database))
            throw new JsonException("Database not specified");

        string? user = (string?)obj["User"];

        if (string.IsNullOrWhiteSpace(user))
            throw new JsonException("User not specified");

        string? pass = (string?)obj["Password"];

        if (string.IsNullOrWhiteSpace(pass))
            throw new JsonException("Password not specified");

        string? version = (string?)obj["Version"];

        if (string.IsNullOrWhiteSpace(version))
            throw new JsonException("Version not specified");

        int dotCount = version
            .Count(c => c == '.');

        if (dotCount != 2)
            throw new FormatException("Version is not in right format");

        return new MSConnectionData()
        {
            Server = server,
            Port = port,
            Database = database,
            User = user,
            Password = pass,
            Version = version
        };
    }

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
