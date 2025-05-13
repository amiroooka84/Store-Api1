using MediatR;
using StoreApi.DAL.Repository.LikeRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.Entity._Like;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.LikeFeature.Query.GetLike
{
    public class GetLikedProductsQueryHandler : IRequestHandler<GetLikedProductsQuery, IEnumerable<Product>>
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IProductRepository _productRepository;

        public GetLikedProductsQueryHandler(ILikeRepository likeRepository, IProductRepository productRepository)
        {
            _likeRepository = likeRepository;
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> Handle(GetLikedProductsQuery request, CancellationToken cancellationToken)
        {
            var likes = _likeRepository.GetUserLikes(request.UserId);
            List<Product> products = new List<Product>();
            foreach (var item in likes)
            {
                products.Add(_productRepository.GetById(item.ProductId));
            }
            return Task.FromResult(products.AsEnumerable());
        }
    }
}
