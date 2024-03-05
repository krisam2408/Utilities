namespace EulePost;

public abstract class IAttachment
{
    public string FileName { get; private set; }
    public byte[] Buffer { get; private set; }
    public abstract string ContentType { get; }

    protected IAttachment(MemoryStream stream, string fileName)
    {
        Buffer = stream.ToArray();
        FileName = fileName;
    }
}
