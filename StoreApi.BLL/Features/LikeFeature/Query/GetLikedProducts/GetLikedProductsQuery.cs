
using MediatR;
using StoreApi.Entity._Like;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Query.GetLike
{
    public class GetLikedProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string UserId { get; set; }
    }
}
