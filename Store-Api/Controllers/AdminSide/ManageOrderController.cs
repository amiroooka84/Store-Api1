using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.OrderFeature.Command.ChangeOrderState;
using StoreApi.BLL.Features.OrderFeature.Query.GetAllOrders;
using StoreApi.BLL.Features.OrderFeature.Query.GetOrderById;
using StoreApi.BLL.Features.OrderFeature.Query.GetOrderProducts;
using StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct;
using StoreApi.Entity._Order;
using StoreApi.Models.FieldsRequest.IDField;

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

    public class ManageOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManageOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Processing , Canceled, Delivered , WaitingPayment

        [HttpGet(Name = "GetNewOrders")]
        public IActionResult GetNewOrders()
        {
            List<Order> res = _mediator.Send(new GetAllOrdersQuery()).Result.ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetCanceledOrders")]
        public IActionResult GetCanceledOrders()
        {         
            List<Order> res = _mediator.Send(new GetAllOrdersQuery()).Result.Where(i=> i.State == Order.state.Canceled).ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetProcessingOrders")]
        public IActionResult GetProcessingOrders()
        {
            List<Order> res = _mediator.Send(new GetAllOrdersQuery()).Result.Where(i => i.State == Order.state.Processing).ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetDeliveredOrders")]
        public IActionResult GetDeliveredOrders()
        {
            List<Order> res = _mediator.Send(new GetAllOrdersQuery()).Result.Where(i => i.State == Order.state.Delivered).ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetWaitingPaymentOrders")]
        public IActionResult GetWaitingPaymentOrders()
        {
            List<Order> res = _mediator.Send(new GetAllOrdersQuery()).Result.Where(i => i.State == Order.state.WaitingPayment).ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetOrderInfo")]
        public IActionResult GetOrderInfo(IntIdField OrderId)
        {
            Order Order = _mediator.Send(new GetOrderByIdQuery() { OrderId = OrderId.id}).Result;
            List<ProductOrder> Products = _mediator.Send(new GetOrderProductsQuery() { OrderId = OrderId.id }).Result.ToList();
            return Ok(new {Order , Products});
        }

        [HttpPost(Name = "ChangeOrderState")]
        public IActionResult ChangeOrderState(int orderId , Order.state OrderState)
        {
            _mediator.Send(new ChangeOrderStateCommand() { OrderId = orderId , OrderState = OrderState});
            return Ok();
        }
    }
}
