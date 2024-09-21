using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Category
{
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Category _Category { get; set; }
    }
}
