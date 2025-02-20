using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Category
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
