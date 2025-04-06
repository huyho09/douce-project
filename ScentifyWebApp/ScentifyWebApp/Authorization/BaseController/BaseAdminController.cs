using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Authorization.BaseController
{
    [AdminAuthorize]
    //[AppTokenAuthorize]
    public class BaseAdminController : Controller
    {
        protected readonly IBaseHttpContext _baseHttpContext;
        protected readonly IConfigurationService _configurationService;
        protected readonly IAppTokenService _appTokenService;


        public BaseAdminController() { }

        public BaseAdminController(IBaseHttpContext baseHttpContext,
                                    IConfigurationService configurationService,
                                    IAppTokenService appTokenService)
        {
            _baseHttpContext = baseHttpContext;
            _configurationService = configurationService;
            _appTokenService = appTokenService;
        }
    }
}
