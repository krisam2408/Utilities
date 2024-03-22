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

    public static BackParameter HandleBackParameter(this string back, string fallbackController, string fallbackAction = "index", string fallbackBackId = "")
    {
        string[] backParts = back.Split('/');
        if(!AideMath.BetweenInc(backParts.Length, 2, 3))
            return new(fallbackController, fallbackAction, fallbackBackId);

        if (backParts.Length == 3)
        {
            BackParameter fullResult = new(backParts[0], backParts[1], backParts[2]);
            return fullResult;
        }

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