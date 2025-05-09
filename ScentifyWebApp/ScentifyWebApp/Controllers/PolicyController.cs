using Microsoft.AspNetCore.Mvc;

namespace ScentifyWebApp.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
