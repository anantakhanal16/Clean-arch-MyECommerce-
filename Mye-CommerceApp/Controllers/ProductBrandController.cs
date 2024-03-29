﻿using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;

namespace Mye_CommerceApp.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IProductBrandRepository _productBrandrepo;

        public ProductBrandController(IProductBrandRepository productBrandRepo)
        {
            _productBrandrepo = productBrandRepo;
        }

        public async Task<IActionResult> Index()
        {
            var productBrands = await _productBrandrepo.GetBrandsAsync();

            List<ProductBrandDto> model = productBrands.Select(item => new ProductBrandDto
            {
                Name = item.Name
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productBrandrepo.GetBrandByIdAsync(id);

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
                await _productBrandrepo.AddBrandAsync(brand);
              
            }

            return RedirectToAction("Index");
        }
    }
}
