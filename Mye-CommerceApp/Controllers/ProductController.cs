using Core.Entites;
using Core.Interface.Repositories;
using Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using Newtonsoft.Json;

namespace Mye_CommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductBrandService _productBrandService;
        private readonly IProductTypeService _productType;
        private readonly IImageUploadRepository _imageService;

        public ProductController(IProductService productService, IProductBrandService productBrandService, IProductTypeService productType, IImageUploadRepository imageService)
        {
            _productService = productService;
            _productBrandService = productBrandService;
            _productType = productType;
            _imageService = imageService;
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

        public async Task<IActionResult> ProductDetails(int id)
        {   
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ProductFromDtos productFromDtos = new ProductFromDtos();

            var serializedData = TempData["ProductFormData"] as string;

            if (!string.IsNullOrEmpty(serializedData))
            {
                productFromDtos = JsonConvert.DeserializeObject<ProductFromDtos>(serializedData) ?? new ProductFromDtos();
            }

            var productType = await _productType.GetProductsTypeAsync();
            var productBrand = await _productBrandService.GetProductsBrandAsync();
            List<ProductTypeDto> Types = productType.Select(item => new ProductTypeDto
            {
                Id = item.Id,
                Name = item.Name
            }).ToList();
            List<ProductBrandDto> Brands = productBrand.Select(item => new ProductBrandDto
            {
                Id = item.Id,
                Name = item.Name
            }).ToList();

            productFromDtos.ProductTypes = Types;
            productFromDtos.ProductBrand = Brands;

            return View(productFromDtos);
        }
        
        [HttpPost]
        public async  Task<IActionResult> AddProductComplete(ProductFromDtos productFromDtos)
        {
           
            productFromDtos.ErrorMessages = Validate(productFromDtos);

            string imageUrl = await _imageService.UploadImageAsync(productFromDtos.Product.PictureFile);

            if (productFromDtos.ErrorMessages.Count == 0)
            {
                var product = new Product
                {
                    Name = productFromDtos.Product.Name,
                    Description = productFromDtos.Product.Description,
                    PictureUrl = imageUrl,
                    Price = productFromDtos.Product.Price,
                    ProductBrandId = productFromDtos.Product.ProductBrandId,
                    ProductTypeId = productFromDtos.Product.ProductTypeId,
                    ProductImageName = productFromDtos.Product.PictureFile.Name,

                };
                await _productService.AddProductAsync(product);
                return RedirectToAction("Index");

            }
            else
            {
                TempData["ProductFormData"] = JsonConvert.SerializeObject(productFromDtos);
                return RedirectToAction("AddProduct");
            }
        }

        public List<string> Validate(ProductFromDtos productFromDtos)
            {
                List<string> errorMessages = new List<string>();

                if (string.IsNullOrEmpty(productFromDtos.Product.Name))
                {
                    errorMessages.Add("Product name is required.");
                }

                if (string.IsNullOrEmpty(productFromDtos.Product.Description))
                {
                    errorMessages.Add("Product description is required.");
                }

                if (string.IsNullOrEmpty(productFromDtos.Product.PictureUrl))
                {
                    errorMessages.Add("Product picture URL is required.");
                }

                if (productFromDtos.Product.Price <= 0)
                {
                    errorMessages.Add("Product price must be greater than zero.");
                }

                if (productFromDtos.Product.ProductBrandId <= 0)
                {
                    errorMessages.Add("Product brand  is required.");
                }

                if (productFromDtos.Product.ProductTypeId <= 0)
                {
                    errorMessages.Add("Product type is required.");
                }
                return errorMessages;
            }

    }
}
