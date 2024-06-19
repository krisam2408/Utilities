namespace MaiSchatz.Abstracts;

public interface IMeinMai
{
    public Task<T?> CallAsync<T>() where T : IResponse;
}
