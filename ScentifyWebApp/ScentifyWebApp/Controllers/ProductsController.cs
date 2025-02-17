using Microsoft.AspNetCore.Mvc;

namespace ScentifyWebApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
