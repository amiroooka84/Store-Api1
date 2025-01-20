using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Account;
using StoreApi.BLL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;
using StoreApi.Models.FieldsRequest.IDField;


namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ManageProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ManageProductController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost(Name = "AddProduct")]
        public IActionResult AddProduct(AddProductFieldRequest productFieldRequest)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            Product product = new Product();
            product = _mapper.Map<AddProductFieldRequest, Product>(productFieldRequest);
            Product res = bl_ManageProduct.AddProduct(product, productFieldRequest.Colors, productFieldRequest.ImagesPath , productFieldRequest.Tags);
            return Ok(res);
        }

        [HttpPut(Name = "EditProduct")]
        public IActionResult EditProduct(EditProductFieldRequest editProductFieldRequest)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            Product product = new Product();
            product = _mapper.Map<EditProductFieldRequest , Product>(editProductFieldRequest);
            Product res = bl_ManageProduct.EditProduct(product, editProductFieldRequest.Colors , editProductFieldRequest.ImagesPath , editProductFieldRequest.Tags);
            return Ok(res);
        }

        [HttpDelete(Name = "DeleteProduct")]
        public IActionResult DeleteProduct(IntIdField id)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            bool res = bl_ManageProduct.DeleteProduct(id.id);
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
        public IActionResult GetProductById(IntIdField id)
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            Product Product = bl_ManageProduct.GetProductById(id.id);
            List<ProductColors> colors = bl_ManageProduct.GetProductColors(id.id);
            List<ProductTag> tags = bl_ManageProduct.GetProductTags(id.id);
            List<ImagePath> images = bl_ManageProduct.GetProductImages(id.id);
            return Ok(new {Product , colors , tags , images});

        }
    }
}
