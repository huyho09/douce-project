using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScentifyWebApp.Models.ViewModels;

namespace ScentifyWebApp.Controllers
{
	public class ContactUsController : Controller
	{

		public IActionResult Index()
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "contactUs.json");

			if (!System.IO.File.Exists(filePath))
			{
				throw new Exception("Product data file not found.");
			}

			var jsonData = System.IO.File.ReadAllText(filePath);
			var contactUsData = JsonConvert.DeserializeObject<ContactUsViewModel>(jsonData);
			return View(contactUsData);
		}

	}
}
