using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Basket
{
    public class Basket
    {
        public int id { get; set; }
        public Product Product  { get; set; }
        public ProductColors  ProductColors { get; set; }
        public User User { get; set; }
        public int Number { get; set; }
    }
}
