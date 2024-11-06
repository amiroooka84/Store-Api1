using StoreApi.DAL.DB;
using StoreApi.Entity._Category;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Admin
{
    public class dl_ManageProduct
    {
        public Product AddProduct(Product product, List<string> imagesPath)
        {
            db db = new db();   
            var ResultProduct = db.Products.Add(product);
            db.SaveChanges();


            foreach (var image in imagesPath)
            {
                ImagePath imagePath = new ImagePath()
                {
                    Image = image,
                    Product = ResultProduct.Entity
                };
                db.ImagesPath.Add(imagePath);
            }
            db.SaveChanges();
            return ResultProduct.Entity;
        }
    }
}
