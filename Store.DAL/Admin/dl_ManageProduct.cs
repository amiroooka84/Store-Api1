using StoreApi.DAL.DB;
using StoreApi.Entity._Category;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreApi.DAL.Admin
{
    public class dl_ManageProduct
    {
        public Product AddProduct(Product product, List<ProductColors> colors, List<string> imagesPath)
        {
            db db = new db();   
            var ResultProduct = db.Products.Add(product);
            db.SaveChanges();
            AddColors(ResultProduct.Entity , colors);
            AddImages(ResultProduct.Entity, imagesPath);
            db.SaveChanges();
            return ResultProduct.Entity;
        }

        public Product EditProduct(Product product, List<ProductColors> colors, List<string> imagesPath)
        {
            db db = new db();
            foreach (var item in db.Products)
            {
                if (item.id == product.id)
                {
                    item.Name = product.Name;
                    item.Brand = product.Brand;
                    item.Slack = product.Slack;
                    item.Code = product.Code;
                    item.Number = product.Number;
                    item.Discount = product.Discount;
                    item.Price = product.Price;
                    item.Description = product.Description;
                    item.ImagePath = product.ImagePath;
                    item.specs = product.specs;
                    item.CategoryId = product.CategoryId;
                    product = item;
                    break;
                }
            }
            RemoveColors(product);
            AddColors(product, colors);
            RemoveImages(product);
            AddImages(product , imagesPath);
            db.SaveChanges();
            return product;
        }

        private void RemoveImages(Product product)
        {
            db db = new db();
            foreach (var item in db.ImagesPath)
            {
                if (item.ProductId == product.id)
                {
                    db.ImagesPath.Remove(item);
                }
            }
            db.SaveChanges();
        }
        private void AddImages(Product product , List<string> imagesPath)
        {
            db db = new db();
            foreach (var image in imagesPath)
            {
                ImagePath imagePath = new ImagePath()
                {
                    Image = image,
                    ProductId = product.id
                };
                db.ImagesPath.Add(imagePath);
            }
            db.SaveChanges();
        }

        private void RemoveColors(Product product)
        {
            db db = new db();
            foreach (var item in db.ProductColors)
            {
                if (item.ProductId == product.id)
                {
                    db.ProductColors.Remove(item);
                }
            }
            db.SaveChanges();
        }
        private void AddColors(Product product, List<ProductColors> productColors)
        {
            db db = new db();
            db.ProductColors.AddRange(productColors);
            db.SaveChanges();
        }

        public bool DeleteProduct(int id)
        {
            db db = new db();
            foreach (var item in db.Products)
            {
                if (item.id == id)
                {
                    db.Products.Remove(item);
                    RemoveImages(item);
                    RemoveColors(item);
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
    }
}
