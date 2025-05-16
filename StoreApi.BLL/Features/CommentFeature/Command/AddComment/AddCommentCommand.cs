using MediatR;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CommentFeature.Command.AddComment
{
    public class AddCommentCommand : IRequest<Comment>
    {
        public Comment Comment { get; set; }
    }
}
