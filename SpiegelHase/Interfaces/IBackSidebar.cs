namespace SpiegelHase.Interfaces;

public interface IBackSidebar
{
    public string BackController { get; set; }
    public string BackAction { get; set; }
    public string? BackId { get; set; }
}
