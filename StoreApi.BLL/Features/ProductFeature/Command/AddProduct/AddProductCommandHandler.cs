﻿using MediatR;
using StoreApi.DAL.Repository.ImagePathRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.DAL.Repository.ProductTagRepository;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.ProductFeature.Command.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductColorsRepository _productColorsRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IImagePathRepository _imagePathRepository;

        public AddProductCommandHandler(IProductRepository productRepository, IProductColorsRepository productColorsRepository, IProductTagRepository productTagRepository, IImagePathRepository imagePathRepository)
        {
            _productRepository = productRepository;
            _productColorsRepository = productColorsRepository;
            _productTagRepository = productTagRepository;
            _imagePathRepository = imagePathRepository;
        }
        public Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var res = _productRepository.Create(request.Product);
            foreach (var color in request.Colors)
            {
                color.ProductId = res.id;
                _productColorsRepository.Create(color);
            }
            foreach (var image in request.ImagesPath)
            {
                _imagePathRepository.Create(new ImagePath() { Image = image.Image, ProductId = res.id });
            }
            foreach (var tag in request.Tags)
            {
                _productTagRepository.Create(new ProductTag() { Tag = tag.Tag, ProductId = res.id });
            }
            return Task.FromResult(res);
        }
    }
}
