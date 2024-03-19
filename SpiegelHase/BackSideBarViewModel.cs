using SpiegelHase.Attributes;
using SpiegelHase.Interfaces;

namespace SpiegelHase;

public class BackSideBarViewModel : BaseViewModel, IBackSidebar
{
    [Ignorable] public string BackController { get; set; }
    [Ignorable] public string BackAction { get; set; }
    [Ignorable] public string? BackId { get; set; }
}
