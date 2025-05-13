using MediatR;
using StoreApi.DAL.Repository.ImagePathRepository;
using StoreApi.DAL.Repository.LikeRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.DAL.Repository.ProductTagRepository;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductColorsRepository _productColorsRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IImagePathRepository _imagePathRepository;

        public GetByIdProductQueryHandler(IProductRepository productRepository, IProductColorsRepository productColorsRepository, IProductTagRepository productTagRepository, IImagePathRepository imagePathRepository, ILikeRepository likeRepository)
        {
            _productRepository = productRepository;
            _productColorsRepository = productColorsRepository;
            _productTagRepository = productTagRepository;
            _imagePathRepository = imagePathRepository;
        }

        public Task<GetByIdProductViewModel> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            GetByIdProductViewModel viewModel = new GetByIdProductViewModel();
            viewModel.Product = _productRepository.GetById(request.id);
            viewModel.Colors = _productColorsRepository.GetByProductId(request.id);
            viewModel.Tags = _productTagRepository.GetByProductId(request.id);
            viewModel.ImagesPath = _imagePathRepository.GetByProductId(request.id);
            return Task.FromResult(viewModel);
        }
    }
}
