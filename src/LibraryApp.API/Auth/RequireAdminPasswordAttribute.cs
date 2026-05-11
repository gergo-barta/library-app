using LibraryApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryApp.API.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class RequireAdminPasswordAttribute : Attribute, IAuthorizationFilter
{
    public const string HeaderName = "X-Admin-Password";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var pwd) ||
            string.IsNullOrWhiteSpace(pwd))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
        if (authService is null || !authService.VerifyAdminPassword(pwd.ToString()))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
