using Core.Entites;
using Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;

namespace Mye_CommerceApp.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productType;

        public ProductTypeController(IProductTypeService productType)
        {
            _productType = productType;
        }

        public async Task<IActionResult> Index()
        {
            var productType = await _productType.GetProductsTypeAsync();

            List<ProductTypeDto> model = productType.Select(item => new ProductTypeDto
            {
                Name = item.Name
            }).ToList();
            
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddProductType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductTypeComplete(string productType)
        {
            ProductType type = new ProductType();
            type.Name = productType;

            await _productType.AddProductTypeAsync(type);
           return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProductType(int id)
        {
            await _productType.DeleteProductTypeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
