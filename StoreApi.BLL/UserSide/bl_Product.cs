using StoreApi.DAL.UserSide;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.UserSide
{
    public class bl_Product
    {
        public Product GetProduct(int id)
        {
            dl_Product dl_Product = new dl_Product();
            return dl_Product.GetProduct(id);
        }
        public List<ProductColors> GetProductColors(int id)
        {
            dl_Product dl_Product = new dl_Product();
            return dl_Product.GetProductColors(id);
        }
        public List<ImagePath> GetProductImages(int id)
        {
            dl_Product dl_Product = new dl_Product();
            return dl_Product.GetProductImages(id);
        }
    }
}
