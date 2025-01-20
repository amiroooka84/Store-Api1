using Microsoft.Data.SqlClient;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreApi.DAL.Admin
{
    public class dl_ManageProduct
    {
        public Product AddProduct(Product product, List<ProductColors> colors, List<string> imagesPath, List<string> tags)
        {
            db db = new db();   
            var ResultProduct = db.Products.Add(product);
            AddColors(ResultProduct.Entity , colors);
            AddImages(ResultProduct.Entity, imagesPath);
            AddTags(ResultProduct.Entity, tags);
            db.SaveChanges();
            return ResultProduct.Entity;
        }



        public Product EditProduct(Product product, List<ProductColors> colors, List<string> imagesPath, List<string> tags)
        {
            db db = new db();
            db.Products.Update(product);
            RemoveColors(product);
            AddColors(product, colors);
            RemoveImages(product);
            AddImages(product , imagesPath);   
            RemoveTags(product);
            AddTags(product , tags);
            db.SaveChanges();
            return product;
        }

        public bool DeleteProduct(int id)
        {
            db db = new db();
            Product product =  db.Products.Remove(new Product() { id = id}).Entity;
            RemoveImages(product);
            RemoveColors(product);
            RemoveTags(product);
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

        public List<Product> GetAllProducts()
        {
            db db = new db();
            return db.Products.ToList();
        }

        public Product GetProductById(int ProductId)
        {
            Product product = new Product(); 
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("select * from dbo.Products where id = " + ProductId + ";", con);
            SqlDataReader rdr;
            try
            {
                rdr = cm.ExecuteReader();

            }
            catch (Exception)
            {
                return null;
            }
            while (rdr.Read())
            {
                product.id = (int)rdr["id"];
                product.Name = rdr["Name"].ToString();
                product.Slack = rdr["Slack"].ToString();
                product.Brand = rdr["Brand"].ToString();
                product.Price = (int)rdr["Price"];
                product.Discount = (int)rdr["Discount"];
                product.ImagePath = rdr["ImagePath"].ToString();
                product.CategoryId = (int)rdr["CategoryId"];
                product.Code = (int)rdr["Code"];
                product.Description = rdr["Description"].ToString();
                product.specs = rdr["specs"].ToString();
                product.Number = (int)rdr["Number"];
            }
            con.Close();
            return product;
        }
        public List<ProductColors> GetProductColors(int ProductId)
        {
            List<ProductColors> productColors = new List<ProductColors>();
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("select * from dbo.ProductColors where ProductId =" + ProductId+ ";", con);
            SqlDataReader rdr;
            try
            {
                rdr = cm.ExecuteReader();

            }
            catch (Exception)
            {
                return null;
            }
            while (rdr.Read())
            {
                ProductColors productColor = new ProductColors();
                productColor.id = (int)rdr["id"];
                productColor.Color = rdr["Color"].ToString();
                productColor.CodeColor = rdr["CodeColor"].ToString();
                productColor.Number = (int)rdr["Number"];
                productColor.Discount = (int)rdr["Discount"];
                productColor.Price = (int)rdr["Price"];
                productColor.ProductId = (int)rdr["ProductId"];
                productColors.Add(productColor);
            }
            con.Close();
            return productColors;
        }

        public List<ProductTag> GetProductTags(int ProductId)
        {
            List<ProductTag> ProductTags = new List<ProductTag>();
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("select * from dbo.ProductTags where ProductId =" + ProductId + ";", con);
            SqlDataReader rdr;
            try
            {
                rdr = cm.ExecuteReader();

            }
            catch (Exception)
            {
                return null;
            }
            while (rdr.Read())
            {
                ProductTag productTag = new ProductTag();
                productTag.id = (int)rdr["id"];
                productTag.Tag = rdr["Tag"].ToString();
                productTag.ProductId = (int)rdr["ProductId"];
                ProductTags.Add(productTag);
            }
            con.Close();
            return ProductTags;
        }

        public List<ImagePath> GetProductImages(int ProductId)
        {
            List<ImagePath> ImagePaths = new List<ImagePath>();
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("select * from dbo.ImagesPath where ProductId =" + ProductId + ";", con);
            SqlDataReader rdr;
            try
            {
                rdr = cm.ExecuteReader();

            }
            catch (Exception)
            {
                return null;
            }
            while (rdr.Read())
            {
                ImagePath imagePath = new ImagePath();
                imagePath.id = (int)rdr["id"];
                imagePath.Image = rdr["Image"].ToString();
                imagePath.ProductId = (int)rdr["ProductId"];
                ImagePaths.Add(imagePath);
            }
            con.Close();
            return ImagePaths;
        }



        ////////////////////////////////////////////

        private void RemoveImages(Product product)
        {
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("delete from dbo.ImagesPath where ProductId = "+product.id+";", con);
            cm.ExecuteNonQuery();
            con.Close();
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
        /////////////
        private void RemoveColors(Product product)
        {
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("delete from dbo.ProductColors where ProductId = " + product.id + ";", con);
            cm.ExecuteNonQuery();
            con.Close();
        }
        private void AddColors(Product product, List<ProductColors> productColors)
        {
            foreach (var item in productColors)
            {
                item.ProductId = product.id;
            }
            db db = new db();
            db.ProductColors.AddRange(productColors);
            db.SaveChanges();
        }
        /////////////
        private void RemoveTags(Product product)
        {
            var con = new SqlConnection(ConStr.con);
            con.Open();
            SqlCommand cm = new SqlCommand("delete from dbo.ProductTags where ProductId = " + product.id + ";", con);
            cm.ExecuteNonQuery();
            con.Close();
        }
        private void AddTags(Product product, List<string> tags)
        {
            db db = new db();
            foreach (var tag in tags)
            {
                ProductTag productTag = new ProductTag()
                {
                    Tag = tag,
                    ProductId = product.id
                };
                db.ProductTags.Add(productTag);
            }
            db.SaveChanges();
        }



    }
}
