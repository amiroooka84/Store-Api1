using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Image
{
    public class ImagePath
    {
        public int id { get; set; }
        public Product Product { get; set; }
        public string Image { get; set; }
    }
}

