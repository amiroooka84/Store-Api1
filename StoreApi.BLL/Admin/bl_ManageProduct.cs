using StoreApi.DAL.Admin;
using StoreApi.Entity._Category;
using StoreApi.Entity._Image;
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
        public Product AddProduct(Product product, List<ProductColors>? colors, List<string>? imagesPath)
        {
            dl_ManageProduct dl_ManageProduct = new dl_ManageProduct();
            return dl_ManageProduct.AddProduct(product ,colors, imagesPath);

        }

        public bool DeleteProduct(int id)
        {
            dl_ManageProduct dl_ManageProduct = new dl_ManageProduct();
            return dl_ManageProduct.DeleteProduct(id);
        }

        public Product EditProduct(Product product, List<ProductColors>? colors, List<string>? imagesPath)
        {
            dl_ManageProduct dl_ManageProduct = new dl_ManageProduct();
            return dl_ManageProduct.EditProduct(product, colors ,imagesPath);
        }
    }
}
