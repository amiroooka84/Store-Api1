using MediatR;
using StoreApi.DAL.Repository.CommentRepository;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CommentFeature.Query.GetProductComments
{
    public class GetProductCommentsCommandHandler : IRequestHandler<GetProductCommentsCommand, IEnumerable<Comment>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetProductCommentsCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public Task<IEnumerable<Comment>> Handle(GetProductCommentsCommand request, CancellationToken cancellationToken)
        {
            var res = _commentRepository.GetProductComments(request.ProductId);
            return Task.FromResult(res);
        }
    }
}
