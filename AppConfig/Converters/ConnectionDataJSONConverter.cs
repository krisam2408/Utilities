using AppConfig.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static AppConfig.Abstract.IConnectionData;

namespace AppConfig.Converters;

public sealed class ConnectionDataJSONConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(IConnectionData);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);

        string? connectionType = (string?)obj["ConnectionType"];

        if (string.IsNullOrWhiteSpace(connectionType))
            throw new JsonException("Connection type not specified");

        if (connectionType == ConnectionTypeName.SQSTrusted.ToString())
            return CastSQSTrustedConnection(obj, connectionType);

        if (connectionType == ConnectionTypeName.PG.ToString())
            return CastPGConnection(obj, connectionType);

        throw new JsonException("A valid deserialization type not found");
    }

    private static SQSTrustedConnectionData CastSQSTrustedConnection(JObject obj, string connectionType)
    {
        string? source = (string?)obj["Source"];

        if (string.IsNullOrWhiteSpace(source))
            throw new JsonException("Source not specified");

        string? database = (string?)obj["Database"];

        if (string.IsNullOrWhiteSpace(database))
            throw new JsonException("Database not specified");

        return new SQSTrustedConnectionData()
        {
            ConnectionType = connectionType,
            Source = source,
            Database = database
        };
    }

    private static PGConnectionData CastPGConnection(JObject obj, string connectionType)
    {
        string? server = (string?)obj["Server"];

        if (string.IsNullOrWhiteSpace(server))
            throw new JsonException("Server not specified");

        string? port = (string?)obj["Port"];

        if (string.IsNullOrWhiteSpace(port))
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
            Port = port,
            Database = database,
            UserId = userId,
            Password = password
        };
    }

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
