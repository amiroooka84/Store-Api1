using MediatR;
using StoreApi.DAL.Repository.LikeRepository;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Command.DisLike
{
    public class DisLikeCommandHandler : IRequestHandler<DisLikeCommand, Like>
    {
        private readonly ILikeRepository _likeRepository;

        public DisLikeCommandHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }
        public Task<Like> Handle(DisLikeCommand request, CancellationToken cancellationToken)
        {
            var res = _likeRepository.DeleteByProductIdAndUserId(request.Like);
            return Task.FromResult(res);
        }
    }
}
