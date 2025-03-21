using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.OrderFeature.Command.AddOrder;
using StoreApi.BLL.Features.OrderFeature.Command.VerifyOrder;
using StoreApi.BLL.Features.OrderFeature.Query.GetByUserIdOrders;
using StoreApi.BLL.Features.OrderFeature.Query.GetOrderById;
using StoreApi.BLL.Features.OrderFeature.Query.GetOrderProducts;
using StoreApi.BLL.Features.ProductFeature.Query.GetAllProducts;
using StoreApi.Entity._Order;
using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using StoreApi.Models.FieldsRequest.IDField;
using StoreApi.Models.FieldsRequest.UserSide.Order;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public OrderController(IMapper mapper, IMediator mediator , UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager; 
        }

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

        [AllowAnonymous]
        [HttpPost(Name = "VerifyOrder")]
        public async Task<IActionResult> VerifyOrder(IntIdField OrderId)
        {
            bool res = await _mediator.Send(new VerifyOrderCommand() { OrderId = OrderId.id});
            return Ok(res);
        }

        [HttpPost(Name = "GetOrders")]
        public IActionResult GetOrders()
        {
            string userId = _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First()).Result.Id;
            List<Order> res = _mediator.Send(new GetByUserIdOrdersQuery() { UserId = userId }).Result.ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetOrderInfoUser")]
        public IActionResult GetOrderInfo(IntIdField OrderId)
        {
            Order Order = _mediator.Send(new GetOrderByIdQuery() { OrderId = OrderId.id }).Result;
            List<ProductOrder> Products = _mediator.Send(new GetOrderProductsQuery() { OrderId = OrderId.id }).Result.ToList();
            return Ok();
        }
    }
}
