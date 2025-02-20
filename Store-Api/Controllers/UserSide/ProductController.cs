using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.IDField;

namespace StoreApi.Controllers.UserSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(IntIdField id)
        {
            GetByIdProductViewModel res = await _mediator.Send(new GetByIdProductQuery() { id = id.id });
            return Ok(res);
        }
    }
}
