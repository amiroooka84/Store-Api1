﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StoreApi.Entity._Category;
using StoreApi.Entity._Product;
using System.Web.Mvc;

namespace StoreApi.Models.FieldsRequest.AdminSide.ManageProduct
{
    public class AddProductFieldRequest
    {

        public string? Name { get; set; }
        public string? Slack { get; set; }
        public int Code { get; set; }
        public string? Brand { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        [AllowHtml]
        public string? Description { get; set; }
        [AllowHtml]
        public string? specs { get; set; }
        public int CategoryId { get; set; }
        public List<ProductColors> Colors { get; set; } 
        public List<string> Tags { get; set; }
        public string? ImagePath { get; set; }

        public List<string> ImagesPath { get; set; }

    }
}
