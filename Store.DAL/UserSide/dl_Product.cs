using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SqlDataReaderMapper;
using StoreApi.DAL.DB;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.UserSide
{
    public class dl_Product
    {
        public Product GetProduct(int id) {
            
            Product product = new Product();
            var con = new SqlConnection(ConStr.con);
            SqlCommand cm = new SqlCommand("select * from Products where Products.id = "+id+";", con);
            con.Open();
            SqlDataReader sqlDataReader = cm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                product = new Product()
                {
                    Name = sqlDataReader["Name"].ToString(),
                    Brand = sqlDataReader["Brand"].ToString(),
                    CategoryId = (int)sqlDataReader["CategoryId"],
                    Code = (int)sqlDataReader["Code"],
                    Description = sqlDataReader["Description"].ToString(),
                    Discount = (int)sqlDataReader["Discount"],
                    id = (int)sqlDataReader["id"],
                    ImagePath = sqlDataReader["ImagePath"].ToString(),
                    Number = (int)sqlDataReader["Number"],
                    Price = (int)sqlDataReader["Price"],
                    Slack = sqlDataReader["Slack"].ToString(),
                    specs = sqlDataReader["specs"].ToString(),
                };
            }
            con.Close();
            return product;
        }

        public List<ProductColors> GetProductColors(int id)
        {
            List<ProductColors> productColors = new List<ProductColors>();
            var con = new SqlConnection(ConStr.con);
            SqlCommand cm = new SqlCommand("select * from ProductColors where ProductColors.ProductId = "+id+";", con);
            con.Open();
            SqlDataReader sqlDataReader = cm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ProductColors productColor = new ProductColors()
                {
                    Discount = (int)sqlDataReader["Discount"],
                    id = (int)sqlDataReader["id"],
                    Number = (int)sqlDataReader["Number"],
                    Price = (int)sqlDataReader["Price"],
                    CodeColor = sqlDataReader["CodeColor"].ToString(),
                    Color = sqlDataReader["Color"].ToString(),
                    ProductId = (int)sqlDataReader["ProductId"]
                };
                productColors.Add(productColor);
            }

            con.Close();
            return productColors;
        }

        public List<ImagePath> GetProductImages(int id)
        {
            List<ImagePath> ImagePaths = new List<ImagePath>();
            var con = new SqlConnection(ConStr.con);
            SqlCommand cm = new SqlCommand("select * from ImagesPath where ImagesPath.ProductId = " + id + ";", con);
            con.Open();
            SqlDataReader sqlDataReader = cm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ImagePath productColor = new ImagePath()
                {
                    id = (int)sqlDataReader["id"],
                    ProductId = (int)sqlDataReader["ProductId"],
                    Image = sqlDataReader["Image"].ToString()
                };
                ImagePaths.Add(productColor);
            }

            con.Close(); 
            return ImagePaths;
        }

        public List<ProductTag> GetProductTags(int id)
        {
            List<ProductTag> ProductTags = new List<ProductTag>();
            var con = new SqlConnection(ConStr.con);
            SqlCommand cm = new SqlCommand("select * from ProductTags where ProductTags.ProductId = " + id + ";", con);
            con.Open();
            SqlDataReader sqlDataReader = cm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ProductTag productTag = new ProductTag()
                {
                    id = (int)sqlDataReader["id"],
                    ProductId = (int)sqlDataReader["ProductId"],
                    Tag = sqlDataReader["Tag"].ToString()
                };
                ProductTags.Add(productTag);
            }
            con.Close();
            return ProductTags;
        }
    }
}
