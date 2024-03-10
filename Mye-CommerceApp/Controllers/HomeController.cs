using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;

namespace Mye_CommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductRepository _productService;
        private readonly IProductBrandRepository _productBrandService;
        private readonly IProductTypeRepository _productTypeService;

        public HomeController(ILogger<HomeController> logger, IProductRepository productService,
             IProductBrandRepository productBrandService
            , IProductTypeRepository productTypeService)
        {
            _logger = logger;
            _productService = productService;
            _productBrandService = productBrandService;
            _productTypeService = productTypeService;
        }

        public async Task<IActionResult> Index(string productTypeFilter, string productBrandFilter, decimal? minPriceFilter, decimal? maxPriceFilter)
        {
            Home model = new Home();

            IEnumerable<Product> products = await _productService.GetProductsAsync(productTypeFilter, productBrandFilter, minPriceFilter, maxPriceFilter);

            IEnumerable<ProductBrand> productBrand = await _productBrandService.GetBrandsAsync();

            IEnumerable<ProductType> productType = await _productTypeService.GetProductTypesAsync();

            model.SelectedProductTypeFilter = productTypeFilter;
            model.SelectedProductBrandFilter = productBrandFilter;
            model.MinPriceFilter = minPriceFilter;
            model.MaxPriceFilter = maxPriceFilter;

            foreach (var product in products)
            {
                var productDto = new ProductListDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    PictureUrl = product.PictureUrl,
                    ProductType = product.ProductType.Name,
                    ProductBrand = product.ProductBrand.Name,
                };
                model.productList.Add(productDto);
            }

            foreach (var pBrand in productBrand)
            {
                var Brands = new ProductBrandList
                {
                    Id = pBrand.Id,
                    Name = pBrand.Name,
                };

                model.productbrandList.Add(Brands);

            }

            foreach (var pType in productType)
            {
                var Type = new ProducttypeList
                {
                    Id = pType.Id,
                    Name = pType.Name,

                };

                model.producttypelist.Add(Type);
            }

            return View(model);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}