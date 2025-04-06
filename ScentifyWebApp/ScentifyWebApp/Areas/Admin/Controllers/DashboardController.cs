using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Authorization.BaseController;
using ScentifyWebApp.Models;
using System.Diagnostics;

namespace ScentifyWebApp.Areas.Admin.Controllers;

public class DashboardController : BaseAdminController
{
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
