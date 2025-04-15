using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScentifyWebApp.Authorization.BaseController;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Models.ViewModels;

namespace ScentifyWebApp.Areas.Admin.Controllers
{
	public class ContactUsAdminController : BaseAdminController
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _environment;

		public ContactUsAdminController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
		{
			_context = context;
			_mapper = mapper;
			_environment = environment;
		}
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
		[HttpPost]
		public async Task<IActionResult> Index(ContactUsViewModel requestDTO)
		{
			if (requestDTO == null)
			{
				return BadRequest("Invalid data received for update.");
			}

			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "contactUs.json");

			try
			{
				var updatedJson = JsonConvert.SerializeObject(requestDTO, Formatting.Indented);
				await System.IO.File.WriteAllTextAsync(filePath, updatedJson);
			}
			catch (Exception ex)
			{
				// Optionally log the error
				return StatusCode(500, "An error occurred while saving the file: " + ex.Message);
			}

			return Redirect("/admin/ContactUsAdmin/index");
		}

	}
}
