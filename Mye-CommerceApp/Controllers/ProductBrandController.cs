using Core.Entites;
using Core.Implementation;
using Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;

namespace Mye_CommerceApp.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IProductBrandService _productBrandService;

        public ProductBrandController(IProductBrandService productBrandService)
        {
            _productBrandService = productBrandService;
        }

        public async Task<IActionResult> Index()
        {
            var productBrands = await _productBrandService.GetProductsBrandAsync();

            List<ProductBrandDto> model = productBrands.Select(item => new ProductBrandDto
            {
                Name = item.Name
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productBrandService.GetProductBrandById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult AddProductBrand()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> AddProductBrandComplete(string brandName)
        {
            ProductBrand brand = new ProductBrand();
            brand.Name= brandName;
            if (ModelState.IsValid)
            {
                await _productBrandService.AddProductBrandAsync(brand);
              
            }

            return RedirectToAction("Index");
        }
    }
}
