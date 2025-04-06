using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScentifyWebApp.Authorization.BaseController;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Areas.Admin.Controllers
{
    //[Route("perfumes")]
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
            var products = await _context.Perfume.ToListAsync();
            var result = new List<DtoPerfume>();
            if (products != null && products.Count > 0)
            {
                result = _mapper.Map<List<DtoPerfume>>(products);
                List<Perfume> randomPerfumes = GetRandomItems(products, 3);

                // get similar 
                var mappingList = _mapper.Map<List<DtoPerfume>>(randomPerfumes);
                // Assign similar perfumes
                if (result != null && result.Any())
                {
                    foreach (var item in result)
                    {
                        //item.SimilarPerfumes = mappingList;
                        if (!string.IsNullOrEmpty(item.Ingredients))
                        {
                            var Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(item.Ingredients);
                            item.DtoIngredients = Ingredients;
                        }
                        if (!string.IsNullOrEmpty(item.PriceInfo))
                        {
                            var PriceInfoData = item.PriceInfo;
                            var productSizes = JsonHelpers.ParseJson<ProductSize>(PriceInfoData);
                            if (productSizes != null && productSizes.Any())
                            {
                                item.ProductSizes = productSizes;
                            }
                        }
                    }
                }
            }
            return View(result);
        }
        //[HttpGet("Create")]
        public IActionResult Create()
        {
            var model = new DtoPerfume();
            return View("Create", model);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(DtoPerfume requestDTO)
        {
            try
            {
                if (requestDTO != null)
                {
                    var request = _mapper.Map<Perfume>(requestDTO);
                    var priceInfo = new List<PriceInfo>();
                    priceInfo.Add(new PriceInfo() { Price = requestDTO.Price1, VolumeMl = requestDTO.VolumeMl1, Currency = requestDTO.Currency });
                    priceInfo.Add(new PriceInfo() { Price = requestDTO.Price2, VolumeMl = requestDTO.VolumeMl2, Currency = requestDTO.Currency });
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

                    _context.Perfume.Add(request);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("index");
                }
                return RedirectToAction("Index", "Product");
            }
            catch { }

            ViewData["ErrorSubmit"] = "Submit failed!";
            return View(requestDTO);
        }

        //[HttpGet("Update")]
        public async Task<IActionResult> Update(string id)
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
                    var ingredients = JsonConvert.SerializeObject(product.Ingredients);
                    if (!string.IsNullOrEmpty(ingredients))
                    {
                        viewModel.DtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(ingredients);
                        if (viewModel.DtoIngredients != null)
                        {
                            viewModel.Ingredient_Img_1 = viewModel.DtoIngredients[1]?.ImageUrl ?? "";
                            viewModel.Ingredient_Img_2 = viewModel.DtoIngredients[2]?.ImageUrl ?? "";
                            viewModel.Ingredient_Img_3 = viewModel.DtoIngredients[3]?.ImageUrl ?? "";
                            viewModel.Ingredient_Img_4 = viewModel.DtoIngredients[4]?.ImageUrl ?? "";
                            viewModel.Ingredient_Name_1 = viewModel.DtoIngredients[1]?.Name ?? "";
                            viewModel.Ingredient_Name_2 = viewModel.DtoIngredients[2]?.Name ?? "";
                            viewModel.Ingredient_Name_3 = viewModel.DtoIngredients[3]?.Name ?? "";
                            viewModel.Ingredient_Name_4 = viewModel.DtoIngredients[4]?.Name ?? "";
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
                        }
                        if (i == 1)
                        {
                            viewModel.Price2 = productSizes[i].Price;
                            viewModel.VolumeMl2 = productSizes[i].VolumeMl;
                            viewModel.Currency = productSizes[i].Currency;
                        }
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(DtoPerfume requestDTO)
        {
            if (requestDTO != null)
            {
                if (!Guid.TryParse(requestDTO.Id, out Guid guidId))
                {
                }
                var existingPerfume = _context.Perfume.FirstOrDefault(m => m.Id == guidId);
                var priceInfo = new List<PriceInfo>();
                priceInfo.Add(new PriceInfo() { Price = requestDTO.Price1, VolumeMl = requestDTO.VolumeMl1, Currency = requestDTO.Currency });
                priceInfo.Add(new PriceInfo() { Price = requestDTO.Price2, VolumeMl = requestDTO.VolumeMl2, Currency = requestDTO.Currency });
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

                _mapper.Map(requestDTO, existingPerfume);
                if (priceInfo != null && priceInfo.Any() && existingPerfume != null)
                {
                    existingPerfume.PriceInfo = JsonConvert.SerializeObject(priceInfo);
                }
                if (fragranceNotes != null && priceInfo != null && existingPerfume != null)
                {
                    existingPerfume.FragranceNotes = JsonConvert.SerializeObject(fragranceNotes);
                }
                await _context.SaveChangesAsync();
                return Redirect("/Products/Update?id=" + requestDTO.Id);
            }
            return RedirectToAction("Index", "Product");
        }

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

            return Content("/Products");
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
