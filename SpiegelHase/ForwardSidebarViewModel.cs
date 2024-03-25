using SpiegelHase.Interfaces;

namespace SpiegelHase;

public class ForwardSidebarViewModel : BackSideBarViewModel, IForwardSidebar
{
    public string ForwardId { get; set; }
}
