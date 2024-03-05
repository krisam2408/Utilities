namespace AppConfig.Abstract;

public interface IDBConnectionData
{
    public static ConnectionTypeName TypeName { get; }
    public string ConnectionType { get; set; }
    public string ConnectionString { get; }

    public enum ConnectionTypeName
    {
        PG,
        SQS,
        SQSTrusted
    }
}
