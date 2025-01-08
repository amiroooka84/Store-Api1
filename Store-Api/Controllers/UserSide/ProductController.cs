using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.UserSide;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.IDField;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet(Name = "GetProduct")]
        public IActionResult GetProduct(IntIdField id)
        {
            bl_Product bl_Product = new bl_Product();
            Product product = bl_Product.GetProduct(id.id);
            List<ImagePath> imagePaths = bl_Product.GetProductImages(id.id);
            List<ProductColors> productColors = bl_Product.GetProductColors(id.id);
            List<ProductTag> productTags = bl_Product.GetProductTags(id.id);
            return Ok(new { product , imagePaths , productColors , productTags});
        }
    }
}
