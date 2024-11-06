using StoreApi.DAL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Admin
{
    public class bl_ManageProduct
    {
        public Product AddProduct(Product product, List<string>? imagesPath)
        {
            dl_ManageProduct dl_ManageProduct = new dl_ManageProduct();
            return dl_ManageProduct.AddProduct(product , imagesPath);

        }
    }
}
