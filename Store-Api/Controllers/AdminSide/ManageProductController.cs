﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using StoreApi.BLL.Features.ProductFeature.Command.AddProduct;
using StoreApi.BLL.Features.ProductFeature.Command.DeleteProduct;
using StoreApi.BLL.Features.ProductFeature.Command.UpdateProduct;
using StoreApi.BLL.Features.ProductFeature.Query.GetAllProducts;
using StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct;
using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;
using StoreApi.Models.FieldsRequest.IDField;
using StoreApi.Models.Services.Redis;

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class ManageProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICacheProvider _cacheProvider;

        public ManageProductController(IMapper mapper, IMediator mediator, IConnectionMultiplexer connection, ICacheProvider cacheProvider)
        {
            _mapper = mapper;
            _mediator = mediator;
            _cacheProvider = cacheProvider;
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
            _cacheProvider.Subscribe("ProductId:"+editProductFieldRequest.id);
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
        public async Task<IActionResult> GetProductById(int id)
        {
            GetByIdProductViewModel res = await _mediator.Send(new GetByIdProductQuery() { id = id});
            return Ok(res);
        }
    }
}
