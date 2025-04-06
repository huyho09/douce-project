using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScentifyWebApp.Helper;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Authorization
{
    public class AdminAuthorizeAttribute : TypeFilterAttribute
    {
        public AdminAuthorizeAttribute() : base(typeof(AdminTokenAuthorizationFilter))
        {
        }

        private class AdminTokenAuthorizationFilter : IAsyncAuthorizationFilter
        {
            public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
            {
                var baseContext = DependencyInjectionHelper.ResolveService<IBaseHttpContext>();
                if (baseContext.HttpContext().User.Identity?.IsAuthenticated == false)
                {
                    // Get the requested path
                    string requestedPath = filterContext?.HttpContext?.Request?.GetEncodedUrl();

                    filterContext.Result = new RedirectResult("/admin/login?returnValue=" + requestedPath);
                    filterContext.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }
        }
    }
}