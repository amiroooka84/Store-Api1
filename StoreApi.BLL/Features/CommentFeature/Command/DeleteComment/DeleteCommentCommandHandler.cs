using MediatR;
using StoreApi.DAL.Repository.CommentRepository;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CommentFeature.Command.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Comment>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public Task<Comment> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _commentRepository.GetById(request.CommentId);
            Comment res = null;
            if (comment.UserId == request.UserId) 
            {
                res = _commentRepository.Delete(request.CommentId);
            }
            return Task.FromResult(res);
        }
    }
}
