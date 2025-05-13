
using MediatR;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Command.GetLike
{
    public class GetLikeCommand : IRequest<Like>
    {
        public Like Like { get; set; }
    }
}
