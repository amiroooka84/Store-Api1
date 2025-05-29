using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.ProductFeature.Command.AddProduct;
using StoreApi.BLL.Features.ProductFeature.Command.DeleteProduct;
using StoreApi.BLL.Features.ProductFeature.Command.UpdateProduct;
using StoreApi.BLL.Features.ProductFeature.Query.GetAllProducts;
using StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;
using StoreApi.Models.FieldsRequest.IDField;

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class ManageProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ManageProductController( IMapper mapper , IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductFieldRequest productFieldRequest)
        {
            AddProductCommand addProductCommand = _mapper.Map<AddProductFieldRequest, AddProductCommand>(productFieldRequest);
            Product product = _mapper.Map<AddProductFieldRequest, Product>(productFieldRequest);
            addProductCommand.Product = product;
            Product res = await _mediator.Send(addProductCommand);
            return Ok(res);
        }

        [HttpPut(Name = "EditProduct")]
        public async Task<IActionResult> EditProduct(EditProductFieldRequest editProductFieldRequest)
        {
            UpdateProductCommand UpdateProductCommand = _mapper.Map<EditProductFieldRequest, UpdateProductCommand>(editProductFieldRequest);
            Product product = _mapper.Map<EditProductFieldRequest, Product>(editProductFieldRequest);
            UpdateProductCommand.Product = product;
            Product res = await _mediator.Send(UpdateProductCommand);
            return Ok(res);
        }

        [HttpDelete(Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(IntIdField id)
        {
            bool res = await _mediator.Send(new DeleteProductCommand() { id = id.id}) == null ? false : true;
            return Ok(res);
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<Product> res = await _mediator.Send(new GetAllProductsQuery());
            return Ok(res);
        }

        [HttpGet(Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(IntIdField id)
        {
            GetByIdProductViewModel res = await _mediator.Send(new GetByIdProductQuery() { id = id.id});
            return Ok(res);

        }
    }
}
