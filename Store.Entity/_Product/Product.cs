using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApi.Entity._Category;

namespace StoreApi.Entity._Product
{
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Slack { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Number { get; set; }
        public string Discription { get; set; }
        public string Specifications { get; set; }
        public string ImagePath { get; set; }
        public Category _Category { get; set; }
        public List<ProductColors> Colors { get; set; }

    }

    public class ProductColors
    {
        public int id { get; set; }
        public string Color { get; set; }
        public string CodeColor { get; set; }
    }
}
