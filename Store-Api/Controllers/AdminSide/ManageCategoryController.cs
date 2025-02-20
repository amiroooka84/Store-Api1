using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.CategoryFeature.Command.AddCategory;
using StoreApi.BLL.Features.CategoryFeature.Command.DeleteCategory;
using StoreApi.BLL.Features.CategoryFeature.Command.UpdateCategory;
using StoreApi.BLL.Features.CategoryFeature.Query.GetAllCategories;
using StoreApi.BLL.Features.CategoryFeature.Query.GetByIdCategory;
using StoreApi.Entity._Category;
using StoreApi.Models.FieldsRequest.AdminSide.ManageCategory;
using StoreApi.Models.FieldsRequest.IDField;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ManageCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ManageCategoryController(IMapper mapper , IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost(Name = "AddCategory")]
        public async Task<IActionResult> AddCategory(AddCategoryFieldRequest CategoryFieldRequest)
        {
            Category category = _mapper.Map<AddCategoryFieldRequest, Category>(CategoryFieldRequest);
            AddCategoryCommand categoryCommand = new AddCategoryCommand() { Category = category};
            Category res = await _mediator.Send(categoryCommand);
            return Ok(res);
        }

        [HttpPut(Name = "EditCategory")]
        public async Task<IActionResult> EditCategory(EditCategoryFieldRequest EditCategoryField)
        {
            Category category = _mapper.Map<EditCategoryFieldRequest, Category>(EditCategoryField);
            UpdateCategoryCommand categoryCommand = new UpdateCategoryCommand() { Category = category };
            Category res = await _mediator.Send(categoryCommand);
            return Ok(res);

        }

        [HttpDelete(Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(IntIdField id)
        {        
            DeleteCategoryCommand categoryCommand = new DeleteCategoryCommand() { id = id.id};
            Category res = await _mediator.Send(categoryCommand);
            return Ok(res);
        }

        [HttpGet(Name = "GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<Category> res = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(res);
        }

        [HttpGet(Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(IntIdField id)
        {
            Category res = await _mediator.Send(new GetByIdCategoryQuery() { id = id.id});
            return Ok(res);
        }
    }
}
