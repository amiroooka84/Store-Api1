﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Account;
using StoreApi.BLL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;


namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ManageProductController : ControllerBase
    {
        [HttpPost(Name = "AddProduct")]
        public IActionResult AddProduct(AddProductFieldRequest productFieldRequest)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            Product product = new Product()
            {
                Name = productFieldRequest.Name,
                Brand = productFieldRequest.Brand,
                Slack = productFieldRequest.Slack,
                Code = productFieldRequest.Code,
                Number = productFieldRequest.Number,
                Discount = productFieldRequest.Discount,
                Price = productFieldRequest.Price,
                Description = productFieldRequest.Description,
                ImagePath = productFieldRequest.ImagePath,
                specs = productFieldRequest.specs,
                CategoryId = productFieldRequest.CategoryId,

            };
            Product res = bl_ManageProduct.AddProduct(product, productFieldRequest.Colors, productFieldRequest.ImagesPath , productFieldRequest.Tags);
            return Ok(res);
        }

        [HttpPost(Name = "EditProduct")]
        public IActionResult EditProduct(EditProductFieldRequest EditProductFieldRequest)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            Product product = new Product()
            {
                id = EditProductFieldRequest.id,
                Name = EditProductFieldRequest.Name,
                Brand = EditProductFieldRequest.Brand,
                Slack = EditProductFieldRequest.Slack,
                Code = EditProductFieldRequest.Code,
                Number = EditProductFieldRequest.Number,
                Discount = EditProductFieldRequest.Discount,
                Price = EditProductFieldRequest.Price,
                Description = EditProductFieldRequest.Description,
                ImagePath = EditProductFieldRequest.ImagePath,
                specs = EditProductFieldRequest.specs,
                CategoryId = EditProductFieldRequest.CategoryId,

            };
            Product res = bl_ManageProduct.EditProduct(product, EditProductFieldRequest.Colors , EditProductFieldRequest.ImagesPath , EditProductFieldRequest.Tags);

            return Ok(res);
        }

        [HttpDelete(Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            bool res = bl_ManageProduct.DeleteProduct(id);
            return Ok(res);
        }


        [HttpGet(Name = "GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            List<Product> res = bl_ManageProduct.GetAllProducts();
            return Ok(res);
            
        }

        [HttpGet(Name = "GetProductById")]
        public IActionResult GetProductById(int id)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            Product Product = bl_ManageProduct.GetProductById(id);
            List<ProductColors> colors = bl_ManageProduct.GetProductColors(id);
            List<ProductTag> tags = bl_ManageProduct.GetProductTags(id);
            List<ImagePath> images = bl_ManageProduct.GetProductImages(id);
            return Ok(new {Product , colors , tags , images});

        }
    }
}
