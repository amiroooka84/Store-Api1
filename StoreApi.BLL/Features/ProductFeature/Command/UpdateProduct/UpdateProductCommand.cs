﻿using MediatR;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public Product Product { get; set; }
        public List<ProductColors> Colors { get; set; }
        public List<ProductTag> Tags { get; set; }
        public List<ImagePath> ImagesPath { get; set; }
    }
}
