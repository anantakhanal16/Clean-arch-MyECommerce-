using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;

namespace Mye_CommerceApp.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepository _productTypeRepo;

        public ProductTypeController(IProductTypeRepository productTypeRepo)
        {
            _productTypeRepo = productTypeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var productType = await _productTypeRepo.GetProductTypesAsync();

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

            await _productTypeRepo.AddProductTypeAsync(type);
           return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProductType(int id)
        {
            await _productTypeRepo.DeleteProductTypeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
