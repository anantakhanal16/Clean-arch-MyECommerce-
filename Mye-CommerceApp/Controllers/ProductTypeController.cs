using Core.Entites;
using Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mye_CommerceApp.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productType;

        public ProductTypeController(IProductTypeService productType)
        {
            _productType = productType;
        }

        public IActionResult Index()
        {
            var productType = _productType.GetProductsTypeAsync();

            return View(productType);
        }
        [HttpGet]
        public async Task<IActionResult> AddProductType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductType(ProductType productType)
        {
            await _productType.AddProductTypeAsync(productType);
           return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProductType(int id)
        {
            await _productType.DeleteProductTypeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
