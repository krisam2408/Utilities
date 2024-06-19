namespace MaiSchatz.Abstracts;

public abstract class IResponse
{
    public int StatusCode { get; set; }
    public string StatusDescription { get; set; }
    public string? ReasonPhrase { get; set; }
}
