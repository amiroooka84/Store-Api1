using StoreApi.DAL.DB;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Admin
{
    public class dl_ManageCategory
    {
        public Category AddCategory(Category category)
        {
            db db = new db();
            var res = db.Categories.Add(category);
            db.SaveChanges();
            return res.Entity;
        }

        public bool DeleteCategory(int id)
        {
            db db = new db();
            foreach (var item in db.Categories)
            {
                if (item.id == id)
                {
                    db.Categories.Remove(item);
                    break;
                }
            }
            var res = db.SaveChanges();
            if (res == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Category EditCategory(Category category)
        {
            db db = new db();
            foreach (var item in db.Categories)
            {
                if (item.id == category.id)
                {
                    item.Name = category.Name;
                    item.ImagePath = category.ImagePath;
                    item.CategoryId = category.CategoryId;
                    category = item;
                    break;
                }
            }
            db.SaveChanges();
            return category;
        }

        public List<Category> GetAllCategories()
        {
            db db = new db ();
            return db.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            db db = new db();
            foreach(var item in db.Categories)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
