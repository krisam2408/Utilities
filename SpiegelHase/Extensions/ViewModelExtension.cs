using Aide;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpiegelHase.DataAnnotations;
using SpiegelHase.DataTransfer;
using SpiegelHase.Interfaces;
using System.ComponentModel;
using System.Reflection;

namespace SpiegelHase.Extensions;

public static class ViewModelExtension
{
    private static readonly Dictionary<string, string> ErrorTranslation = new()
    {
        { "0", "Ha ocurrido un error" },
        { "field is required", "El campo &apos;{0}&apos; es requerido" },
        { "e-mail address", "El campo &apos;{0}&apos; tiene que ser un correo electrónico válido" },
        { "rut format", "El campo &apos;{0}&apos; no es un Rut válido" }
    };

    private static string HandleMessage(string errorMessage)
    {
        foreach (KeyValuePair<string, string> kv in ErrorTranslation)
            if (errorMessage.Contains(kv.Key))
                return kv.Value;
        return ErrorTranslation["0"];
    }

    public static void SetModelMessages(this BaseViewModel model, ModelStateDictionary modelState)
    {
        foreach (KeyValuePair<string, ModelStateEntry> kv in modelState)
        {
            if (kv.Value.ValidationState == ModelValidationState.Valid)
                continue;

            string display = GetPropertyDisplay(model, kv.Key);

            foreach (ModelError error in kv.Value.Errors)
            {
                string message = HandleMessage(error.ErrorMessage);
                string result = string.Format(message, display);
                model.AddErrorMessage(result);
            }
        }
    }

    private static string GetPropertyDisplay(BaseViewModel model, string propertyName)
    {
        PropertyInfo? key = model
            .GetType()
            .GetProperty(propertyName);

        if (key == null)
            return propertyName;

        DisplayNameAttribute? attr = key.GetCustomAttribute<DisplayNameAttribute>();

        if (attr == null)
            return propertyName;

        return attr.DisplayName;
    }

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

    public static BackParameter HandleBackParameter(this string back, string fallbackController, string fallbackAction = "index", string fallbackBackId = "")
    {
        string[] backParts = back.Split('/');
        if(!AideMath.BetweenInc(backParts.Length, 2, 3))
            return new(fallbackController, fallbackAction, fallbackBackId);

        if (backParts.Length == 3)
            return new(backParts[0], backParts[1], backParts[2]);

        return new(backParts[0], backParts[1]);
    }

    public static BackParameter HandleBackParameter(this string back, BackParameter fallback)
    {
        string[] backParts = back.Split('/');
        if(!AideMath.BetweenInc(backParts.Length, 2, 3))
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