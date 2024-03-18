using SpiegelHase.Interfaces;

namespace SpiegelHase;

public class BackSideBarViewModel : BaseViewModel, IBackSidebar
{
    public string BackController { get; set; }
    public string BackAction { get; set; }
    public string? BackId { get; set; }
}
