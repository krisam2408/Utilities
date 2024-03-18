namespace SpiegelHase.Interfaces;

public interface IPagination<T>
{
    public T[] List { get; set; }
    public int CurrentPage { get; set; }
    public int MaxPages { get; set; }
}
