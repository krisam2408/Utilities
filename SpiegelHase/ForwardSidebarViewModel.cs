using SpiegelHase.DataTransfer;
using SpiegelHase.Interfaces;

namespace SpiegelHase;

public class ForwardSidebarViewModel : BackSideBarViewModel, IForwardSidebar
{
    public string ForwardId { get; set; }

    public void SetForwardSidebarModel(string forwardId, string controllerName, string actionName = "index", string backId = "")
    {
        ForwardId = forwardId;
        SetBackSidebarModel(controllerName, actionName, backId);
    }

    public void SetForwardSidebarModel(ForwardParameter parameter)
    {
        ForwardId = parameter.ForwardId;
        SetBackSidebarModel(parameter);
    }

}
