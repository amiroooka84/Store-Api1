using MediatR;
using StoreApi.DAL.Repository.ImagePathRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.DAL.Repository.ProductTagRepository;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductColorsRepository _productColorsRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IImagePathRepository _imagePathRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository, IProductColorsRepository productColorsRepository, IProductTagRepository productTagRepository, IImagePathRepository imagePathRepository)
        {
            _productRepository = productRepository;
            _productColorsRepository = productColorsRepository;
            _productTagRepository = productTagRepository;
            _imagePathRepository = imagePathRepository;
        }
        public Task<Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var res = _productRepository.Delete(request.id);
            _imagePathRepository.DeleteByProductId(request.id);
            _productColorsRepository.DeleteByProductId(request.id);
            _productTagRepository.DeleteByProductId(request.id);
            return Task.FromResult(res);
        }
    }
}
