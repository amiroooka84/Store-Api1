using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct
{
    public class GetByIdProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductColors> Colors { get; set; }
        public IEnumerable<ProductTag> Tags { get; set; }
        public IEnumerable<ImagePath> ImagesPath { get; set; }
        public bool IsLiked { get; set; }

    }
}
