using MediatR;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CommentFeature.Command.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Comment>
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
    }
}
