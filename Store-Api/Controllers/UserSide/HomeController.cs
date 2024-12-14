using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Admin;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name ="SpecialProducts")]
        public IActionResult SpecialProducts()
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            var res = bl_ManageProduct.GetAllProducts().OrderByDescending(i => i.Discount).Take(10);
            return Ok(res);
        }

        [HttpGet(Name = "NewProducts")]
        public IActionResult NewProducts()
        {
            bl_ManageProduct bl_ManageProduct = new bl_ManageProduct();
            var res = bl_ManageProduct.GetAllProducts().TakeLast(10);
            return Ok(res);
        }

        [HttpGet(Name = "Categories")]
        public IActionResult Categories()
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            var res = bl_ManageCategory.GetAllCategories();
            return Ok(res);
        }
    }
}
