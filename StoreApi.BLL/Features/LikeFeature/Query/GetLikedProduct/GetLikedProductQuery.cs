using MediatR;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Query.GetLikedProduct
{
    public class GetLikedProductQuery : IRequest<Like>
    {
        public Like Like { get; set; }
    }
}
