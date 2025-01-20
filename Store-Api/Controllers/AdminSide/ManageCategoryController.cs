using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Models.FieldsRequest.AdminSide.ManageCategory;
using StoreApi.Models.FieldsRequest.IDField;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ManageCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ManageCategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost(Name = "AddCategory")]
        public IActionResult AddCategory(AddCategoryFieldRequest CategoryFieldRequest)
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            Category category = new Category();
            category = _mapper.Map<AddCategoryFieldRequest, Category>(CategoryFieldRequest);
            Category res = bl_ManageCategory.AddCategory(category);
            return Ok(res);
        }

        [HttpPut(Name = "EditCategory")]
        public IActionResult EditCategory(EditCategoryFieldRequest EditCategoryField)
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            Category category = new Category();
            category = _mapper.Map<EditCategoryFieldRequest, Category>(EditCategoryField);
            Category res = bl_ManageCategory.EditCategory(category);
            return Ok(res);
        }

        [HttpDelete(Name = "DeleteCategory")]
        public IActionResult DeleteCategory(IntIdField id)
        {        
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            bool res = bl_ManageCategory.DeleteCategory(id.id);
            return Ok(res);
        }

        [HttpGet(Name = "GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            List<Category> res = bl_ManageCategory.GetAllCategories();
            return Ok(res);
        }

        [HttpGet(Name = "GetCategoryById")]
        public IActionResult GetCategoryById(IntIdField id)
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            Category res = bl_ManageCategory.GetCategoryById(id.id);
            return Ok(res);
        }
    }
}
