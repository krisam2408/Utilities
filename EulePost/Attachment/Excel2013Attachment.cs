namespace EulePost.Attachment;

public sealed class Excel2013Attachment : IAttachment
{
    public Excel2013Attachment(MemoryStream stream, string fileName) : base(stream, fileName) { }
    public override string ContentType => "application/vnd.ms-excel";
}
