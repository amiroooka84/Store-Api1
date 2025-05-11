using MediatR;
using StoreApi.DAL.Repository.LikeRepository;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Command.NewFolder.AddLike
{
    public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand, Like>
    {
        private readonly ILikeRepository _likeRepository;

        public AddLikeCommandHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public Task<Like> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            var res = _likeRepository.Create(request.Like);
            return Task.FromResult(res);
        }
    }
}
