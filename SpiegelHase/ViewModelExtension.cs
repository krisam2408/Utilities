using Aide;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpiegelHase.DataAnnotations;
using SpiegelHase.DataTransfer;
using SpiegelHase.Interfaces;
using System.Reflection;

namespace SpiegelHase;

public static class ViewModelExtension
{
    public static void RemoveIgnorable(this ModelStateDictionary modelState, BaseViewModel model)
    {
        PropertyInfo[] properties = model
            .GetType()
            .GetProperties();

        List<string> ignorable = new();
        foreach (PropertyInfo property in properties)
        {
            IgnorableAttribute? attr = property
                .GetCustomAttribute<IgnorableAttribute>();

            if (attr != null)
                ignorable.Add(property.Name);
        }

        foreach (string name in ignorable)
            modelState.Remove(name);
    }

    public static string GetBackParameter(this IBackSidebar model)
    {
        List<string> parts = new()
        {
            model.BackController,
            model.BackAction
        };

        if(!string.IsNullOrWhiteSpace(model.BackId))
            parts.Add(model.BackId);

        string result = "";
        foreach(string part in parts)
            result += $"/{part}";

        return result;
    }

    public static void SetBackSidebarModel(this IBackSidebar model, string controllerName, string actionName = "index", string backId = "")
    {
        model.BackController = controllerName;
        model.BackAction = actionName;
        model.BackId = backId;
        if (string.IsNullOrWhiteSpace(backId))
            model.BackId = null;
    }

    public static void SetBackSidebarModel(this IBackSidebar model, BackParameter parameter)
    {
        model.BackController = parameter.BackController;
        model.BackAction = parameter.BackAction;
        model.BackId = parameter.BackId;
    }

    public static void SetForwardSidebarModel(this IForwardSidebar model, string forwardId, string controllerName, string actionName = "index", string backId = "")
    {
        model.ForwardId = forwardId;
        model.SetBackSidebarModel(controllerName, actionName, backId);
    }

    public static void SetForwardSidebarModel(this IForwardSidebar model, ForwardParameter parameter)
    {
        model.ForwardId = parameter.ForwardId;
        model.SetBackSidebarModel(parameter);
    }

    public static BackParameter HandleBackParameter(this string? back, string fallbackController, string fallbackAction = "index", string fallbackBackId = "")
    {
        if (string.IsNullOrWhiteSpace(back))
            return new(fallbackController, fallbackAction, fallbackBackId);

        string[] backParts = back.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if(backParts.Length == 0 )
            return new(fallbackController, fallbackAction, fallbackBackId);

        BackParameter result;

        if(backParts.Length == 2)
        {
            result = new(backParts[0], backParts[1]);
            return result;
        }

        if (backParts.Length == 3)
        {
            result = new(backParts[0], backParts[1], backParts[2]);
            return result;
        }

        result = new(backParts[0]);
        return result;
    }

    public static BackParameter HandleBackParameter(this string back, BackParameter fallback)
    {
        string[] backParts = back.Split('/');
        if (!backParts.Length.BetweenInc(2, 3))
            return fallback;

        if (backParts.Length == 3)
            return new(backParts[0], backParts[1], backParts[2]);

        return new(backParts[0], backParts[1]);
    }

    public static void SetCustomSidebarModel(this ICustomSidebar model, string sidebarUrl)
    {
        model.CustomSidebar = $"~/Views/{sidebarUrl}.cshtml";
    }

    public static void SetPaginationModel<T>(this IPagination<T> model, int currentPage, int itemsPerPage)
    {
        model.TotalItems = model.List.Length;
        int start = (currentPage - 1) * itemsPerPage;
        int end = start + itemsPerPage;
        Range range = new(start, end);

        int totalItems = model.List.Length;
        int maxPages = (int)MathF.Ceiling((float)totalItems / itemsPerPage);

        T[] filter = model
            .List
            .Take(range)
            .ToArray();

        model.List = filter;
        model.CurrentPage = currentPage;
        model.MaxPages = maxPages;
    }

    public static async Task SetUserRolesModel<TType, TKey>(this IUserRoles<TType> model, TKey key, Func<TKey, Task<List<TType>>> predicate)
    {
        List<TType> roles = await predicate.Invoke(key);

        model.Roles = roles;
    }

    public static bool ContainsRole<T>(this IUserRoles<T> roles, params T[] roleNames)
    {
        foreach (T roleName in roleNames)
            if (roles.Roles.Contains(roleName))
                return true;
        return false;
    }
}