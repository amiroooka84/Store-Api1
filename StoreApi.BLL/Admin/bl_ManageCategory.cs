using StoreApi.DAL.Admin;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Admin
{
    public class bl_ManageCategory
    {
        public Category AddCategory(Category category)
        {
            dl_ManageCategory dl_ManageCategory = new dl_ManageCategory();
            return dl_ManageCategory.AddCategory(category);
        }

        public bool DeleteCategory(int id) 
        {
            dl_ManageCategory dl_ManageCategory = new dl_ManageCategory();
            return dl_ManageCategory.DeleteCategory(id);
        }

        public Category EditCategory(Category category)
        {
            dl_ManageCategory dl_ManageCategory = new dl_ManageCategory();
            return dl_ManageCategory.EditCategory(category);
        }

        public List<Category> GetAllCategories()
        {
            dl_ManageCategory dl_ManageCategory = new dl_ManageCategory();
            return dl_ManageCategory.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            dl_ManageCategory dl_ManageCategory = new dl_ManageCategory();
            return dl_ManageCategory.GetCategoryById(id);
        }
    }
}
