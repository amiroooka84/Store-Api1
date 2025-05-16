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
using StoreApi.Models.FieldsRequest.UserSide.Product;
using StoreApi.Entity._Comment;
using StoreApi.BLL.Features.CommentFeature.Command.AddComment;
using System.Globalization;
using StoreApi.BLL.Service.ConvertDate;
using StoreApi.BLL.Features.CommentFeature.Query.GetProductComments;
using StoreApi.BLL.Features.CommentFeature.Command.DeleteComment;

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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "AddComment")]
        public async Task<IActionResult> AddComment(AddCommentField commentField)
        {
            string phoneNumber = User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Comment comment = new Comment() {
                Content = commentField.Content,
                UserId = user.Id,
                ProductId = commentField.ProductId,
            };
            Comment res = await _mediator.Send(new AddCommentCommand() { Comment = comment});
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete(Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(IntIdField commentId)
        {
            string phoneNumber = User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Comment res = await _mediator.Send(new DeleteCommentCommand() { CommentId = commentId.id , UserId = user.Id });
            return Ok(res);
        }

        [HttpGet(Name = "GetComments")]
        public async Task<IActionResult> GetComments(IntIdField productId)
        {
            IEnumerable<Comment> res = await _mediator.Send(new GetProductCommentsCommand() { ProductId = productId.id });
            return Ok(res);
        }
    }
}
