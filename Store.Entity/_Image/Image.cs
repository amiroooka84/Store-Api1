using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreApi.Entity._Image
{
    public class ImagePath : EntityBase
    {
        public int ProductId { get; set; }

        public string Image { get; set; }
    }
}

