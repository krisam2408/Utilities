using SpiegelHase.DataAnnotations;
using SpiegelHase.DataTransfer;
using SpiegelHase.Interfaces;

namespace SpiegelHase;

public class BackSideBarViewModel : BaseViewModel, IBackSidebar
{
    [Ignorable] public string BackController { get; set; }
    [Ignorable] public string BackAction { get; set; }
    [Ignorable] public string? BackId { get; set; }

    public void SetBackSidebarModel(string controllerName, string actionName = "index", string backId = "")
    {
        BackController = controllerName;
        BackAction = actionName;
        BackId = backId;
        if (string.IsNullOrWhiteSpace(backId))
            BackId = null;
    }

    public void SetBackSidebarModel(BackParameter parameter)
    {
        BackController = parameter.BackController;
        BackAction = parameter.BackAction;
        BackId = parameter.BackId;
    }
}
