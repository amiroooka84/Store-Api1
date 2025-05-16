using MediatR;
using StoreApi.BLL.Service.ConvertDate;
using StoreApi.DAL.Repository.CommentRepository;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CommentFeature.Command.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Comment>
    {
        private readonly ICommentRepository _commentRepository;

        public AddCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<Comment> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            request.Comment.Date = ConvertDateTime.ConvertMiladiToShamsi(DateTime.Today, "yyyy/MM/dd");
            var res = _commentRepository.Create(request.Comment);
            return Task.FromResult(res);
        }
    }
}
