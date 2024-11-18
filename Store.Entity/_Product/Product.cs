using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Brand { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string specs { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }


    }

    public class ProductColors
    {
        public int id { get; set; }
        public string Color { get; set; }
        public string CodeColor { get; set; }
        public int ProductId { get; set; }

    }

    public  class ProductTag
    {
        public int id { get; set; }
        public string Tag { get; set; }
        public int ProductId { get; set; }
    }
}
