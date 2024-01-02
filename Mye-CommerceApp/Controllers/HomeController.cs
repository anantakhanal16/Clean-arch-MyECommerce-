using Core.Entites;
using Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using Mye_CommerceApp.Models;
using System.Diagnostics;

namespace Mye_CommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productService.GetProductsAsync();

            List<ProductListDto> newproducts = new List<ProductListDto>();

            foreach (var product in products)
            {
                var productDto = new ProductListDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    PictureUrl = product.PictureUrl,
                    ProductType = product.ProductType.Name,
                    ProductBrand = product.ProductBrand.Name,
                };

                newproducts.Add(productDto);
            }
            return View(newproducts);
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
}