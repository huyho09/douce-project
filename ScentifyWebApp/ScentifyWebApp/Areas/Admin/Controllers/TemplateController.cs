using Microsoft.AspNetCore.Mvc;

namespace ScentifyWebApp.Areas.Admin.Controllers
{
    public class TemplateController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public TemplateController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile htmlFile)
        {
            if (htmlFile == null || htmlFile.Length == 0)
            {
                TempData["ResultPopup"] = "[Error] Please select a valid HTML file.";
                return RedirectToAction("Index", "Dashboard");
            }

            var uploadsDir = Path.Combine(_env.WebRootPath, "email-templates");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            //var fileName = Path.GetFileName(htmlFile.FileName);
            var fileName = Path.GetFileName("order-confirmation.html");
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await htmlFile.CopyToAsync(stream);
            }

            TempData["ResultPopup"] = $"New Email Template uploaded successfully!";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult UseDefault()
        {
            var defaultFilePath = Path.Combine(_env.WebRootPath, "email-templates   `    ", "default-order-confirmation.html");
            var uploadFilePath = Path.Combine(_env.WebRootPath, "email-templates", "order-confirmation.html");

            if (!System.IO.File.Exists(defaultFilePath))
            {
                TempData["ResultPopup"] = "[Error] Default template not found.";
                return RedirectToAction("Index", "Dashboard");
            }

            try
            {
                // Read default content
                var defaultContent = System.IO.File.ReadAllText(defaultFilePath);

                // Overwrite upload template with default content
                System.IO.File.WriteAllText(uploadFilePath, defaultContent);

                TempData["ResultPopup"] = "Default template has been successfully applied and replaced the current upload.";
            }
            catch (Exception ex)
            {
                TempData["ResultPopup"] = $"[Error] Failed to apply default template: {ex.Message}";
            }

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
