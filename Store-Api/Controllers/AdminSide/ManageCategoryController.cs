using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Models.FieldsRequest.AdminSide.ManageCategory;
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
        [HttpPost(Name = "AddCategory")]
        public IActionResult AddCategory(AddCategoryFieldRequest CategoryFieldRequest)
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            Category category = new Category()
            {
                Name = CategoryFieldRequest.Name,
                CategoryId = CategoryFieldRequest.CategoryId,
                ImagePath = CategoryFieldRequest.ImagePath,
            };
            Category res = bl_ManageCategory.AddCategory(category);
            return Ok(res);
        }

        [HttpPost(Name = "EditCategory")]
        public IActionResult EditCategory(EditCategoryFieldRequest editCategoryField)
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            Category category = new Category()
            {
                id = editCategoryField.CategoryId,
                Name = editCategoryField.Name,
                ImagePath= editCategoryField.ImagePath,
                CategoryId= editCategoryField.CategoryId,
            };
            Category res = bl_ManageCategory.EditCategory(category);
            return Ok(res);
        }

        [HttpDelete(Name = "DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {        
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            bool res = bl_ManageCategory.DeleteCategory(id);
            return Ok(res);
        }

        [HttpGet(Name = "GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            List<Category> res = bl_ManageCategory.GetAllCategories();
            return Ok(res);
        }

        [HttpDelete(Name = "GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            bl_ManageCategory bl_ManageCategory = new bl_ManageCategory();
            Category res = bl_ManageCategory.GetCategoryById(id);
            return Ok(id);
        }
    }
}
