using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.OrderFeature.Command.AddOrder;
using StoreApi.BLL.Features.ProductFeature.Query.GetAllProducts;
using StoreApi.Entity._Order;
using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using StoreApi.Models.FieldsRequest.UserSide.Order;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public OrderController(IMapper mapper, IMediator mediator , UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager; 
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderFieldRequest fieldRequest)
        {       
            AddOrderCommand orderCommand = new AddOrderCommand();
            OrderRequest orderRequest = new OrderRequest();
            
            orderRequest.FullName = fieldRequest.FullName;
            orderRequest.PhoneNumber = fieldRequest.PhoneNumber;
            orderRequest.Address = fieldRequest.Address;
            orderRequest.User = _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First()).Result.Id;
            foreach (var item in fieldRequest.Products)
            {
                var p = new ProductOrderRequest() { ColorId = item.ColorId, ProductId = item.ProductId, Number = item.Number };
                List<ProductOrderRequest> productOrderRequests = new List<ProductOrderRequest>();
                productOrderRequests.Add(p);
                orderCommand.ProductsOrder = productOrderRequests;
            }
            orderCommand.Order = orderRequest;
            Order res = await _mediator.Send(orderCommand);
            return Ok(res);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "VerifyOrder")]
        public async Task<IActionResult> VerifyOrder(CreateOrderFieldRequest fieldRequest)
        {
            AddOrderCommand orderCommand = new AddOrderCommand();
            OrderRequest orderRequest = new OrderRequest();

            orderRequest.FullName = fieldRequest.FullName;
            orderRequest.PhoneNumber = fieldRequest.PhoneNumber;
            orderRequest.Address = fieldRequest.Address;
            orderRequest.User = _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First()).Result.Id;
            foreach (var item in fieldRequest.Products)
            {
                var p = new ProductOrderRequest() { ColorId = item.ColorId, ProductId = item.ProductId, Number = item.Number };
                List<ProductOrderRequest> productOrderRequests = new List<ProductOrderRequest>();
                productOrderRequests.Add(p);
                orderCommand.ProductsOrder = productOrderRequests;
            }
            orderCommand.Order = orderRequest;
            Order res = await _mediator.Send(orderCommand);
            return Ok(res);
        }
    }
}
