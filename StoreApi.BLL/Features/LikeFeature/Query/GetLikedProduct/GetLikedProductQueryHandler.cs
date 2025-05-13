using MediatR;
using StoreApi.DAL.Repository.LikeRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Query.GetLikedProduct
{
    public class GetLikedProductQueryHandler : IRequestHandler<GetLikedProductQuery, Like>
    {
        private readonly ILikeRepository _likeRepository;

        public GetLikedProductQueryHandler(ILikeRepository likeRepository, IProductRepository productRepository)
        {
            _likeRepository = likeRepository;
        }
        public Task<Like> Handle(GetLikedProductQuery request, CancellationToken cancellationToken)
        {
            var res = _likeRepository.GetByProductIdAndUserId(request.Like);
            return Task.FromResult(res);
        }
    }
}
