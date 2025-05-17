using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScentifyWebApp.Authorization.BaseController;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Helper;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;
using System.IO;

namespace ScentifyWebApp.Areas.Admin.Controllers
{
    //[Route("Perfumes")]
    public class PerfumesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public PerfumesController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        //[HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Perfume.AsNoTracking().Select(p => new DtoPerfume
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                ShortDescription = p.ShortDescription,
                Brand = p.Brand,
                //ImageUrl = p.ImageUrl,
                TopPerfumed = p.TopPerfumed,
                MiddlePerfumed = p.MiddlePerfumed,
                BasePerfumed = p.BasePerfumed,
                DtoIngredients = !string.IsNullOrWhiteSpace(p.Ingredients)
                            ? JsonConvert.DeserializeObject<List<Ingredient>>(p.Ingredients)
                            : new List<Ingredient>(),
                ProductSizes = !string.IsNullOrWhiteSpace(p.PriceInfo)
                    ? JsonHelpers.ParseJson<ProductSize>(p.PriceInfo)
                    : new List<ProductSize>()
            })
            .ToListAsync();

            //await ConvertAndStoreImagesAsync();

            // check image save
            //foreach(var product in products)
            //{
            //    if (product.ProductSizes != null && product.ProductSizes.Any())
            //    {
            //        var i = 0;
            //        //foreach (var size in product.ProductSizes)
            //        //{
            //        //    if (!string.IsNullOrEmpty(size.ImageUrl))
            //        //    {
            //        //        var imagePath = Path.Combine(_environment.WebRootPath, "image-test");
            //        //        if (!System.IO.File.Exists(imagePath))
            //        //        {
            //        //            ImageOptimizer.SaveAsPng(size.ImageUrl.Split("base64,")[1], product.Name + "_" + i, imagePath);
            //        //            i++;
            //        //        }
            //        //    }
            //        //}

            //        //foreach (var size in product.ProductSizes)
            //        //{
            //        //    if (!string.IsNullOrEmpty(size.ImageUrl))
            //        //    {
            //        //        var imagePath = Path.Combine(_environment.WebRootPath, "image-test");
            //        //        if (!System.IO.File.Exists(imagePath))
            //        //        {
            //        //            ImageOptimizer.CompressAndSaveImage(size.ImageUrl.Split("base64,")[1], product.Name + "_" + i, imagePath);
            //        //            i++;
            //        //        }
            //        //    }
            //        //}
            //    }
            //}

            //ImageOptimizer.CompressAndSaveAllImages(
            //    @"D:\Sources\douce-project\ScentifyWebApp\ScentifyWebApp\wwwroot\image-test",
            //    @"D:\Sources\douce-project\ScentifyWebApp\ScentifyWebApp\wwwroot\image-output"
            //);
            //ImageOptimizer.CompressAndSaveAllImages(
            //    @"D:\Sources\douce-project\ScentifyWebApp\ScentifyWebApp\wwwroot\images\BANNER",
            //    @"D:\Sources\douce-project\ScentifyWebApp\ScentifyWebApp\wwwroot\image-output"
            //);

            return View(products);
        }

        public async Task ConvertAndStoreImagesAsync(List<Perfume> perfumes)
        {
            try
            {
                // 1) Load all perfumes with their children
                //var perfumes = await _context.Perfume
                //    .ToListAsync();

                string baseFolder = Path.Combine(_environment.WebRootPath, "uploads");

                foreach (var perfume in perfumes)
                {
                    // 2) Handle PriceInfo images
                    var priceInfo = JsonHelpers.ParseJson<ProductSize>(perfume.PriceInfo);

                    await ProcessImageCollectionAsync(
                        items: priceInfo,
                        folder: Path.Combine(baseFolder, "prices"),
                        fileNamePrefix: perfume.Name.Replace(" ", ""),
                        updateUrl: (pi, url) => pi.ImageUrl = url
                    );

                    perfume.PriceInfo = JsonConvert.SerializeObject(priceInfo);

                    var Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(perfume.Ingredients);
                    // 3) Handle Ingredient images
                    await ProcessImageCollectionAsync(
                        items: Ingredients ?? new(),
                        folder: Path.Combine(baseFolder, "ingredients"),
                        fileNamePrefix: perfume.Name.Replace(" ", ""),
                        updateUrl: (ing, url) => ing.ImageUrl = url
                    );
                    perfume.Ingredients = JsonConvert.SerializeObject(Ingredients);

                    var DescriptionImages = JsonConvert.DeserializeObject<List<string>>(perfume.DescriptionImages) ?? new List<string>();
                    var updateDescriptionImages = new List<string>();
                    if (DescriptionImages.Count > 0)
                    {
                        updateDescriptionImages.AddRange(DescriptionImages.Where(m => m.Contains("upload")));// check path exist.
                    }

                    await ProcessImageCollectionAsync(
                        items: DescriptionImages ?? new(),
                        folder: Path.Combine(baseFolder, "description-images"),
                        fileNamePrefix: perfume.Name.Replace(" ", ""), // optional name prefix
                        updateUrl: (item, url) => updateDescriptionImages.Add(url) // update with new URL
                    );
                    perfume.DescriptionImages = JsonConvert.SerializeObject(updateDescriptionImages);
                }

                // 4) Save all URL updates in one go
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Processes a collection of items that have Base64 ImageUrl strings,
        /// writes each image to disk, and calls back to update the entity's ImageUrl.
        /// </summary>
        private async Task ProcessImageCollectionAsync<T>(
            IEnumerable<T> items,
            string folder,
            string fileNamePrefix,
            Action<T, string> updateUrl
        ) where T : class
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            int counter = 0;
            foreach (var item in items)
            {
                // via reflection or interface, get the Base64 string:
                var prop = item.GetType().GetProperty("ImageUrl");
                var base64 = prop?.GetValue(item) as string;
                if (prop == null)
                {
                    base64 = item.ToString();
                }
                if (string.IsNullOrWhiteSpace(base64) || base64.Contains("uploads"))
                {
                    counter++;
                    continue;
                }
                var raw = "";
                if (!base64.Contains("base64,"))
                {
                    raw = base64;
                }
                else
                {
                    raw = base64.Substring(base64.IndexOf("base64,") + 7);
                }

                // decode after comma
                var bytes = Convert.FromBase64String(raw);

                // generate unique name
                var fileName = $"{SanitizeFileName(fileNamePrefix)}_{counter++}.png";
                var filePath = Path.Combine(folder, fileName);
                var relativeUrl = Path.Combine("/uploads", Path.GetFileName(folder), fileName)
                                      .Replace("\\", "/");

                // write file
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);

                // optionally compress/optimize here:
                // ImageOptimizer.CompressAndSave(filePath);

                // update the entity's ImageUrl field to the new URL
                updateUrl(item, relativeUrl);
            }
        }

        /// <summary>
        /// Removes invalid URL/path characters from a string.
        /// </summary>
        private string SanitizeFileName(string input)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                input = input.Replace(c, '_');
            return input;
        }

        //[HttpGet("Create")]
        public IActionResult Create()
        {
            var model = new DtoPerfume();
            return View("Create", model);
        }

        //[HttpPost("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(DtoPerfume requestDTO)
        {
            try
            {
                // convert to base 64
                if (requestDTO != null)
                {
                    var request = _mapper.Map<Perfume>(requestDTO);
                    var priceInfo = new List<PriceInfo>();
                    priceInfo.Add(new PriceInfo() { Price = requestDTO.Price1, VolumeMl = requestDTO.VolumeMl1, Currency = requestDTO.Currency, ImageUrl = requestDTO.ImageUrl1 });
                    priceInfo.Add(new PriceInfo() { Price = requestDTO.Price2, VolumeMl = requestDTO.VolumeMl2, Currency = requestDTO.Currency, ImageUrl = requestDTO.ImageUrl2 });
                    request.PriceInfo = JsonConvert.SerializeObject(priceInfo);
                    var fragranceNotes = new FragranceNote();
                    fragranceNotes.Fruity = requestDTO.Fruity;
                    fragranceNotes.Citrus = requestDTO.Citrus;
                    fragranceNotes.Floral = requestDTO.Floral;
                    fragranceNotes.Woody = requestDTO.Woody;
                    fragranceNotes.Musky = requestDTO.Musky;
                    fragranceNotes.Oriental = requestDTO.Oriental;
                    fragranceNotes.Spicy = requestDTO.Spicy;
                    fragranceNotes.Tobacco = requestDTO.Tobacco;
                    fragranceNotes.Gourmand = requestDTO.Gourmand;
                    request.FragranceNotes = JsonConvert.SerializeObject(fragranceNotes);

                    var ingredients = new List<Ingredient>()
                {
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_1 ?? "",
                        Name = requestDTO.Ingredient_Name_1 ?? ""
                    },
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_2 ?? "",
                        Name = requestDTO.Ingredient_Name_2 ?? ""
                    },
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_3 ?? "",
                        Name = requestDTO.Ingredient_Name_3 ?? ""
                    },
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_4 ?? "",
                        Name = requestDTO.Ingredient_Name_4 ?? ""
                    }
                };

                    request.Ingredients = JsonConvert.SerializeObject(ingredients);

                    var descriptionImages = new List<string>();
                    foreach (var file in requestDTO.DescriptionImageFiles)
                    {
                        if (file != null && file.Length > 0)
                        {
                            using var ms = new MemoryStream();
                            file.CopyTo(ms);

                            var fileBytes = ms.ToArray();

                            // If you want base64 string
                            var base64 = Convert.ToBase64String(fileBytes);
                            descriptionImages.Add(base64);
                            // Save or process as needed
                        }
                    }
                    request.DescriptionImages = JsonConvert.SerializeObject(descriptionImages);

                    request.CreatedAt = DateTime.Now;

                    _context.Perfume.Add(request);

                    await ConvertAndStoreImagesAsync(new List<Perfume> { request });
                    //await _context.SaveChangesAsync();
                    return RedirectToAction("index");
                }
            }
            catch { }

            ViewData["ErrorSubmit"] = "Submit failed!";
            return View(requestDTO);
        }

        //[HttpGet("Update")]
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guidId))
                {
                }
                var product = _context.Perfume.FirstOrDefault(m => m.Id == guidId);
                var viewModel = _mapper.Map<DtoPerfume>(product);
                if (product != null && !string.IsNullOrEmpty(product.PriceInfo))
                {
                    var PriceInfoData = product.PriceInfo;
                    var productSizes = JsonHelpers.ParseJson<ProductSize>(PriceInfoData);
                    if (!string.IsNullOrEmpty(product.FragranceNotes))
                    {
                        var fragranceNotes = JsonHelpers.ParseJson<FragranceNote>(product.FragranceNotes);
                        if (fragranceNotes != null && fragranceNotes.Any())
                        {
                            viewModel.Citrus = fragranceNotes[0].Citrus;
                            viewModel.Floral = fragranceNotes[0].Floral;
                            viewModel.Fruity = fragranceNotes[0].Fruity;
                            viewModel.Woody = fragranceNotes[0].Woody;
                            viewModel.Musky = fragranceNotes[0].Musky;
                            viewModel.Oriental = fragranceNotes[0].Oriental;
                            viewModel.Spicy = fragranceNotes[0].Spicy;
                            viewModel.Tobacco = fragranceNotes[0].Tobacco;
                            viewModel.Gourmand = fragranceNotes[0].Gourmand;
                        }
                        if (!string.IsNullOrEmpty(product.Ingredients))
                        {
                            viewModel.DtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(product.Ingredients);
                            if (viewModel.DtoIngredients != null)
                            {
                                viewModel.Ingredient_Img_1 = viewModel.DtoIngredients.Count > 0 ? viewModel.DtoIngredients[0]?.ImageUrl ?? "" : "";
                                viewModel.Ingredient_Img_2 = viewModel.DtoIngredients.Count > 1 ? viewModel.DtoIngredients[1]?.ImageUrl ?? "" : "";
                                viewModel.Ingredient_Img_3 = viewModel.DtoIngredients.Count > 2 ? viewModel.DtoIngredients[2]?.ImageUrl ?? "" : "";
                                viewModel.Ingredient_Img_4 = viewModel.DtoIngredients.Count > 3 ? viewModel.DtoIngredients[3]?.ImageUrl ?? "" : "";

                                viewModel.Ingredient_Name_1 = viewModel.DtoIngredients.Count > 0 ? viewModel.DtoIngredients[0]?.Name ?? "" : "";
                                viewModel.Ingredient_Name_2 = viewModel.DtoIngredients.Count > 1 ? viewModel.DtoIngredients[1]?.Name ?? "" : "";
                                viewModel.Ingredient_Name_3 = viewModel.DtoIngredients.Count > 2 ? viewModel.DtoIngredients[2]?.Name ?? "" : "";
                                viewModel.Ingredient_Name_4 = viewModel.DtoIngredients.Count > 3 ? viewModel.DtoIngredients[3]?.Name ?? "" : "";
                            }

                        }
                    }

                    if (productSizes != null && productSizes.Any())
                    {
                        viewModel.ProductSizes = productSizes;
                        for (var i = 0; i < productSizes.Count(); i++)
                        {
                            if (i == 0)
                            {
                                viewModel.Price1 = productSizes[i].Price;
                                viewModel.VolumeMl1 = productSizes[i].VolumeMl;
                                viewModel.Currency = productSizes[i].Currency;
                                viewModel.ImageUrl1 = productSizes[i].ImageUrl;
                            }
                            if (i == 1)
                            {
                                viewModel.Price2 = productSizes[i].Price;
                                viewModel.VolumeMl2 = productSizes[i].VolumeMl;
                                viewModel.Currency = productSizes[i].Currency;
                                viewModel.ImageUrl2 = productSizes[i].ImageUrl;
                            }
                        }
                    }
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        //[HttpPost("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(DtoPerfume requestDTO)
        {
            //if (requestDTO == null || !ModelState.IsValid)
            //{
            //    ViewData["ErrorSubmit"] = "Invalid data submitted.";
            //    return View(requestDTO ?? new DtoPerfume());
            //}

            if (requestDTO != null)
            {
                if (!Guid.TryParse(requestDTO.Id, out Guid guidId))
                {
                }
                var existingPerfume = _context.Perfume.FirstOrDefault(m => m.Id == guidId);
                var priceInfo = new List<PriceInfo>();
                priceInfo.Add(new PriceInfo() { Price = requestDTO.Price1, VolumeMl = requestDTO.VolumeMl1, Currency = requestDTO.Currency, ImageUrl = requestDTO.ImageUrl1 });
                priceInfo.Add(new PriceInfo() { Price = requestDTO.Price2, VolumeMl = requestDTO.VolumeMl2, Currency = requestDTO.Currency, ImageUrl = requestDTO.ImageUrl2 });
                var fragranceNotes = new FragranceNote();
                fragranceNotes.Fruity = requestDTO.Fruity;
                fragranceNotes.Citrus = requestDTO.Citrus;
                fragranceNotes.Floral = requestDTO.Floral;
                fragranceNotes.Woody = requestDTO.Woody;
                fragranceNotes.Musky = requestDTO.Musky;
                fragranceNotes.Oriental = requestDTO.Oriental;
                fragranceNotes.Spicy = requestDTO.Spicy;
                fragranceNotes.Tobacco = requestDTO.Tobacco;
                fragranceNotes.Gourmand = requestDTO.Gourmand;

                var ingredients = new List<Ingredient>()
                {
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_1 ?? "",
                        Name = requestDTO.Ingredient_Name_1 ?? ""
                    },
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_2 ?? "",
                        Name = requestDTO.Ingredient_Name_2 ?? ""
                    },
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_3 ?? "",
                        Name = requestDTO.Ingredient_Name_3 ?? ""
                    },
                    new Ingredient()
                    {
                        ImageUrl = requestDTO.Ingredient_Img_4 ?? "",
                        Name = requestDTO.Ingredient_Name_4 ?? ""
                    }
                };

                requestDTO.Ingredients = JsonConvert.SerializeObject(ingredients);

                //if (requestDTO.DescriptionImageFiles?.Count > 0)
                //{
                //    var descriptionImages = new List<string>();
                //    foreach (var file in requestDTO.DescriptionImageFiles)
                //    {
                //        if (file != null && file.Length > 0)
                //        {
                //            using var ms = new MemoryStream();
                //            file.CopyTo(ms);

                //            var fileBytes = ms.ToArray();

                //            // If you want base64 string
                //            var base64 = Convert.ToBase64String(fileBytes);

                //            descriptionImages.Add(base64);
                //            // Save or process as needed
                //        }
                //    }

                //    if (requestDTO.SaveImageIndexChange.Count > 0)
                //    {
                //        var ind = 0;
                //        foreach (var index in requestDTO.SaveImageIndexChange)
                //        {
                //            requestDTO.ListDescriptionImages[index] = descriptionImages[ind];
                //            ind++;
                //        }
                //    }

                //    existingPerfume.DescriptionImages = JsonConvert.SerializeObject(descriptionImages);
                //}

                //if (requestDTO.SaveImageIndexChange.Count > 0)
                //{
                //    var ListDescriptionImages = requestDTO.ListDescriptionImages;
                //    foreach (var index in requestDTO.SaveImageIndexChange)
                //    {
                //        var ind = index - 1;
                //        ListDescriptionImages = ListDescriptionImages
                //                .Where((item, index) => index != ind)
                //                .ToList();
                //        //requestDTO.ListDescriptionImages = new List<string>();
                //        //.AddRange(t);
                //    }
                //    existingPerfume.DescriptionImages = JsonConvert.SerializeObject(ListDescriptionImages);
                //}

                requestDTO.CreatedAt = existingPerfume.CreatedAt;
                _mapper.Map(requestDTO, existingPerfume);
                if (priceInfo != null && priceInfo.Any() && existingPerfume != null)
                {
                    existingPerfume.PriceInfo = JsonConvert.SerializeObject(priceInfo);
                }
                if (fragranceNotes != null && priceInfo != null && existingPerfume != null)
                {
                    existingPerfume.FragranceNotes = JsonConvert.SerializeObject(fragranceNotes);
                }

                await ConvertAndStoreImagesAsync(new List<Perfume> { existingPerfume });

                //await _context.SaveChangesAsync();
            }
            return Redirect("/admin/perfumes/Update?id=" + requestDTO?.Id);
            //return RedirectToAction("Index", "perfumes");
        }

        //[HttpDelete("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string currentId)
        {
            if (!Guid.TryParse(currentId, out Guid guidId))
            {
            }
            var product = _context.Perfume.FirstOrDefault(m => m.Id == guidId);
            if (product != null)
            {
                _context.Perfume.Remove(product);
                await _context.SaveChangesAsync();
            }

            return Content("/admin/perfumes");
        }

        [HttpPost("/api/image/upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string productId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            try
            {
                // Define the directory path: wwwroot/img/{product_id}
                var uploadsDir = Path.Combine(_environment.WebRootPath, "img", productId);
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Generate a unique file name to avoid conflicts
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Generate the URL for the saved image
                var imageUrl = $"/img/{productId}/{fileName}";

                // Return the URL to the client
                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error uploading image", error = ex.Message });
            }
        }

        private List<T> GetRandomItems<T>(List<T> list, int count)
        {
            return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }

        private bool IsValidJson(string str)
        {
            str = str.Trim();
            if ((str.StartsWith("{") && str.EndsWith("}")) || // Object
                (str.StartsWith("[") && str.EndsWith("]")))   // Array
            {
                try
                {
                    JToken.Parse(str);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }
            return false;
        }

    }
}
