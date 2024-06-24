using Microsoft.AspNetCore.Mvc;

namespace SpiegelHase;

public abstract class HaseController : Controller
{
    protected virtual void CommonDataTransfer()
    {

    }

    protected virtual Task CommonDataTransferAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual new ViewResult View()
    {
        CommonDataTransfer();
        return base.View();
    }

    protected virtual async Task<ViewResult> ViewAsync()
    {
        await CommonDataTransferAsync();
        return base.View();
    }

    protected virtual new ViewResult View(string viewName)
    {
        CommonDataTransfer();
        return base.View(viewName);
    }

    protected virtual async Task<ViewResult> ViewAsync(string viewName)
    {
        await CommonDataTransferAsync();
        return base.View(viewName);
    }

    protected virtual new ViewResult View(object model)
    {
        CommonDataTransfer();
        return base.View(model);
    }

    protected virtual async Task<ViewResult> ViewAsync(object model)
    {
        await CommonDataTransferAsync();
        return base.View(model);
    }

    protected virtual new ViewResult View(string viewName, object model)
    {
        CommonDataTransfer();
        return base.View(viewName, model);
    }

    protected virtual async Task<ViewResult> ViewAsync(string viewName, object model)
    {
        await CommonDataTransferAsync();
        return base.View(viewName, model);
    }

    protected virtual new RedirectToActionResult RedirectToAction(string action, object model)
    {
        if (model is GetViewModel getModel)
        {
            getModel.Clear();
        }

        return base.RedirectToAction(action, model);
    }

    protected virtual new RedirectToActionResult RedirectToAction(string action, string controller, object model)
    {
        if (model is GetViewModel getModel)
        {
            getModel.Clear();
        }

        return base.RedirectToAction(action, controller, model);
    }
}
