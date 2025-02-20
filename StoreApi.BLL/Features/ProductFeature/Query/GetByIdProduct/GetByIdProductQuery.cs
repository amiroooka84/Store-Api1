using MediatR;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct
{
    public class GetByIdProductQuery : IRequest<GetByIdProductViewModel>
    {
        public int id { get; set; }
    }
}
