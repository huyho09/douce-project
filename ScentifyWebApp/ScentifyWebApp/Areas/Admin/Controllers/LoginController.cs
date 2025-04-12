using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Helper;
using ScentifyWebApp.Infrastructure.Constants;
using ScentifyWebApp.Infrastructure.ModelConfigurations;
using ScentifyWebApp.Models.Actors;
using ScentifyWebApp.Models.Requests;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IBaseHttpContext _baseHttpContext;
        private readonly IConfigurationService _configurationService;
        private readonly IAppTokenService _appTokenService;

        public LoginController(IBaseHttpContext baseHttpContext,
                                    IConfigurationService configurationService,
                                    IAppTokenService appTokenService)
        {
            _configurationService = configurationService;
            _baseHttpContext = baseHttpContext;
            _appTokenService = appTokenService;
        }

        public IActionResult Index()
        {
            if (_baseHttpContext.HttpContext().User.Identity?.IsAuthenticated == false)
                return View();
            return Redirect("/Admin/Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> Invoke(LoginRequest request)
        {
            try
            {
                if (FieldRequiredHelper.AreFieldsRequired(request))
                {
                    var findAdmin = await CheckExistAccount(request);

                    if (findAdmin.IsSuccess)
                    {
                        // set current user to session (save)
                        _baseHttpContext.SetSession("LOGIN_RESULT", findAdmin);
                        TempData["ResultPopup"] = findAdmin.Detail;

                        if (!string.IsNullOrEmpty(request.ReturnValue))
                        {
                            return Redirect(request.ReturnValue);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = findAdmin.Detail;
                        return View("~/Areas/Admin/Views/Login/Index.cshtml");
                    }
                }
                return View("~/Areas/Admin/Views/Login/Index.cshtml");
            }
            catch (Exception ex)
            {
                // TODO
                throw new Exception(ex.Message);
            }
        }

        private async Task<AppResponseResult<AppUser>> CheckExistAccount(LoginRequest request)
        {
            var result = new AppResponseResult<AppUser>();


            var adminAccount = _configurationService.GetConfigValue<AppUser>(ConfigKeyConstant.ADMIN_ACCOUNT_KEY);
            if (adminAccount != null)
            {
                if (request.UserName == adminAccount.Username
                    && request.Password == adminAccount.Password)
                {
                    // set role temp (TODO)
                    var timeExpiration = request.IsRememberMe == false ? 60 * 2 : 60 * 24; // 2 hours or 24 hours
                    adminAccount.RoleName = "Admin";
                    await _appTokenService.SignInAsync(adminAccount, timeExpiration); // Sign in

                    return result.BuildSuccess(adminAccount, ConfigKeyConstant.INF_MSG_LOGIN_SUCCESS);
                }
            }

            return result.BuildError(ConfigKeyConstant.ERR_MSG_LOGIN_FAILED);
        }
    }
}
