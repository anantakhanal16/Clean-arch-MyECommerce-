using Core.Entites;
using Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var productBrand  = _productBrandService.GetProductsBrandAsync();

            return View(productBrand);
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
        public async Task<IActionResult> AddProductBrand(ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                await _productBrandService.AddProductBrandAsync(productBrand);
                return RedirectToAction(nameof(Index));
            }

            return View(productBrand);
        }
    }
}
