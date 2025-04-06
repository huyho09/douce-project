using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Controllers
{
    public class LogOutController : Controller
    {
        private readonly IBaseHttpContext _baseHttpContext;
        private readonly IConfigurationService _configurationService;
        private readonly IAppTokenService _appTokenService;

        public LogOutController(IBaseHttpContext baseHttpContext,
                                    IConfigurationService configurationService,
                                    IAppTokenService appTokenService)
        {
            _configurationService = configurationService;
            _baseHttpContext = baseHttpContext;
            _appTokenService = appTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Invoke()
        {
            try
            {
                var urlRedirect = "/admin/login";
                if (User.Identity?.IsAuthenticated == true)
                {
                    // Logout
                    await _appTokenService.Logout();
                    _baseHttpContext.ClearAllSession();

                    //if (UserRoleHelper.IsAdminRole() == true)
                    //{
                    //    urlRedirect = "/admin/login";
                    //}
                }

                // TODO return view if admin or staff
                return Json(urlRedirect);
            }
            catch (Exception ex)
            {
                // TODO
                throw new Exception(ex.Message);
            }
        }
    }
}
