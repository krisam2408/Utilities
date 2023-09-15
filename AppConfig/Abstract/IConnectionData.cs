namespace AppConfig.Abstract;

public interface IConnectionData
{
    public static ConnectionTypeName TypeName { get; }
    public string ConnectionType { get; set; }
    public string ConnectionString { get; }

    public enum ConnectionTypeName
    {
        PG,
        SQSTrusted
    }
}
