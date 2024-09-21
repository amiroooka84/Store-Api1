using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Interests
{
    public class Interests
    {
        public long id { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
