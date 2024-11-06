using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Account;
using StoreApi.BLL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;


namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize]
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
                Colors = productFieldRequest.Colors,
                ImagePath = productFieldRequest.ImagePath,
                specs = productFieldRequest.specs,
                CategoryId = productFieldRequest.CategoryId,

            };
            Product res = bl_ManageProduct.AddProduct(product, productFieldRequest.ImagesPath);

            return Ok(res);


        }

    }
}
