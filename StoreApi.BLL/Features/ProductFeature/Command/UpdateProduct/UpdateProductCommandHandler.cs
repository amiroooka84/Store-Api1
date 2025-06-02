using MediatR;
using StoreApi.DAL.Repository.ImagePathRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.DAL.Repository.ProductTagRepository;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductColorsRepository _productColorsRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IImagePathRepository _imagePathRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, IProductColorsRepository productColorsRepository, IProductTagRepository productTagRepository, IImagePathRepository imagePathRepository)
        {
            _productRepository = productRepository;
            _productColorsRepository = productColorsRepository;
            _productTagRepository = productTagRepository;
            _imagePathRepository = imagePathRepository;
        }
        public  Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var res = _productRepository.Update(request.Product);
            _productColorsRepository.DeleteByProductId(request.Product.id);
            _imagePathRepository.DeleteByProductId(request.Product.id);
            _productTagRepository.DeleteByProductId(request.Product.id);
            foreach (var color in request.Colors)
            {
                color.ProductId = request.Product.id;
                _productColorsRepository.Create(color);
            }
            foreach (var image in request.ImagesPath)
            {
                _imagePathRepository.Create(new ImagePath() { Image = image.Image, ProductId = request.Product.id });
            }
            foreach (var tag in request.Tags)
            {
                _productTagRepository.Create(new ProductTag() { Tag = tag.Tag, ProductId = request.Product.id });
            }
            return Task.FromResult(res); ;
        }
    }
}
