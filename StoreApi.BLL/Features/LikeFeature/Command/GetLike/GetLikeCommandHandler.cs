using MediatR;
using StoreApi.DAL.Repository.LikeRepository;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Command.GetLike
{
    public class GetLikeCommandHandler : IRequestHandler<GetLikeCommand, Like>
    {
        private readonly ILikeRepository _likeRepository;

        public GetLikeCommandHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }
        public Task<Like> Handle(GetLikeCommand request, CancellationToken cancellationToken)
        {
            var res = _likeRepository.GetByProductIdAndUserId(request.Like);
            return Task.FromResult(res);
        }
    }
}
