using AutoMapper;
using ClosedXML;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApi.BLL.Features.OrderFeature.Command.AddOrder;
using StoreApi.BLL.Features.OrderFeature.Command.VerifyOrder;
using StoreApi.BLL.Features.OrderFeature.Query.GetByUserIdOrders;
using StoreApi.BLL.Features.OrderFeature.Query.GetOrderById;
using StoreApi.BLL.Features.OrderFeature.Query.GetOrderProducts;
using StoreApi.Entity._Order;
using StoreApi.Entity._User;
using StoreApi.Models.Classes.Payment;
using StoreApi.Models.FieldsRequest.AccountField;
using StoreApi.Models.FieldsRequest.IDField;
using StoreApi.Models.FieldsRequest.UserSide.Order;
using StoreApi.Models.Services.Payment;

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
        [HttpGet(Name = "VerifyOrder")]
        public async Task<IActionResult> VerifyOrder(int success, long trackId, int orderId, int status)
        {
            var client = new HttpClient();
            string data = JsonConvert.SerializeObject(new { merchant = "zibal", trackId = trackId });
            var content = new StringContent(
                       data,
            System.Text.Encoding.UTF8, "application/json");

            var verifyRes = await  client.PostAsync("https://gateway.zibal.ir/v1/verify" , content);
            var inquiryRes = await client.PostAsync("https://gateway.zibal.ir/v1/inquiry", content);

            //var request = new HttpRequestMessage(HttpMethod.Post, "https://gateway.zibal.ir/v1/inquiry");
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://gateway.zibal.ir/v1/verify");
            //string data = JsonConvert.SerializeObject(new { merchant = "zibal", trackId = 4596035231 });
            //request.Content = new StringContent(
            //           data,
            //System.Text.Encoding.UTF8, "application/json");
            //var response = client.SendAsync(request);
            //Console.WriteLine(response.Content.ReadAsStringAsync());
            //var pv = await Payment.PaymentVerify(trackId);
            //Console.Write(pv);
            //if (pv.result == 100) 
            //{ 


            //}

            PaymentVerify a = JsonConvert.DeserializeObject<PaymentVerify>(inquiryRes.Content.ReadAsStream().ToString()!)!;

            //if (a.)
            //{

            //}
            bool res = false; /*await _mediator.Send(new VerifyOrderCommand() { OrderId = orderId});*/

            return Ok(inquiryRes.Content.ReadAsStream());
        }

        [HttpGet(Name = "GetOrders")]
        public IActionResult GetOrders()
        {
            string userId = _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First()).Result.Id;
            List<Order> res = _mediator.Send(new GetByUserIdOrdersQuery() { UserId = userId }).Result.ToList();
            return Ok(res);
        }

        [HttpGet(Name = "GetOrderInfoUser")]
        public IActionResult GetOrderInfo(int order)
        {
            Order Order = _mediator.Send(new GetOrderByIdQuery() { OrderId = order }).Result;
            List<ProductOrder> Products = _mediator.Send(new GetOrderProductsQuery() { OrderId = order }).Result.ToList();
            return Ok(new{ Order , Products });
        }
    }
}
