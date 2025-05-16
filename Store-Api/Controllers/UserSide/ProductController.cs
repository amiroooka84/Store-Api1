using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct;
using StoreApi.Entity._Image;
using StoreApi.Entity._Like;
using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using StoreApi.Models.FieldsRequest.IDField;
using StoreApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using StoreApi.BLL.Features.LikeFeature.Query.GetLike;
using StoreApi.BLL.Features.LikeFeature.Query.GetLikedProduct;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public ProductController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpGet(Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(IntIdField id)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var result = await HttpContext.AuthenticateAsync("Bearer");
            bool isLike = false;
            if (token != null)
            {
                string phoneNumber = result.Principal.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
                User user = await _userManager.FindByNameAsync(phoneNumber);
                Like like = new Like()
                {
                    UserId = user.Id,
                    ProductId = id.id,
                };
                Like likeRes = await _mediator.Send(new GetLikedProductQuery() { Like = like });

                 isLike =  likeRes != null ? true : false;
            }
            await _mediator.Send(new GetByIdProductQuery() { id = id.id });
            GetByIdProductViewModel res = await _mediator.Send(new GetByIdProductQuery() { id = id.id });
            res.IsLiked = isLike;
            return Ok(res);
        }


    }
}
