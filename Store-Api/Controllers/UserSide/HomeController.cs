using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.CategoryFeature.Query.GetAllCategories;
using StoreApi.BLL.Features.ProductFeature.Query.GetAllProducts;
using StoreApi.Entity._Category;
using StoreApi.Entity._Product;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name ="SpecialProducts")]
        public async Task<IActionResult> SpecialProducts()
        {
            IEnumerable<Product> res = await _mediator.Send(new GetAllProductsQuery());
            return Ok(res.OrderByDescending(i => i.Discount).Take(10));
        }

        [HttpGet(Name = "NewProducts")]
        public async Task<IActionResult> NewProducts()
        {
            IEnumerable<Product> res = await _mediator.Send(new GetAllProductsQuery());
            return Ok(res.TakeLast(10));
        }

        [HttpGet(Name = "Categories")]
        public async Task<IActionResult> Categories()
        {
            IEnumerable<Category> res = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(res);
        }
    }
}
