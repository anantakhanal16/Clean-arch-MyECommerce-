﻿using Core.Entites;

namespace Mye_CommerceApp.Dtos
{
    public class ProductListDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
        
    }
}