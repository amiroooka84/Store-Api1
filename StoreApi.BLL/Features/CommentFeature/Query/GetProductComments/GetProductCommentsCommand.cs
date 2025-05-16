using MediatR;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CommentFeature.Query.GetProductComments
{
    public class GetProductCommentsCommand : IRequest<IEnumerable<Comment>>
    {
        public int ProductId { get; set; }
    }
}
