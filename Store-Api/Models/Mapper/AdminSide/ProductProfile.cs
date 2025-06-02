using AutoMapper;
using StoreApi.BLL.Features.ProductFeature.Command.AddProduct;
using StoreApi.BLL.Features.ProductFeature.Command.UpdateProduct;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;

namespace StoreApi.Models.Mapper.AdminSide
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ImagePathFieldRequest, ImagePath>();
            CreateMap<ProductColorFieldRequest, ProductColors>();
            CreateMap<ProductTagFieldRequest, ProductTag>();



            CreateMap<AddProductFieldRequest, AddProductCommand>();
            CreateMap<AddProductFieldRequest, Product>();               
            //.ForMember(dest => dest.Product.Name, act => act.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Product.Slack, act => act.MapFrom(src => src.Slack))
            //.ForMember(dest => dest.Product.Number, act => act.MapFrom(src => src.Number))
            //.ForMember(dest => dest.Product.specs, act => act.MapFrom(src => src.specs))
            //.ForMember(dest => dest.Product.Brand, act => act.MapFrom(src => src.Brand))
            //.ForMember(dest => dest.Product.CategoryId, act => act.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.Product.Code, act => act.MapFrom(src => src.Code))
            //.ForMember(dest => dest.Product.Description, act => act.MapFrom(src => src.Description))
            //.ForMember(dest => dest.Product.Discount, act => act.MapFrom(src => src.Discount))
            //.ForMember(dest => dest.Product.ImagePath, act => act.MapFrom(src => src.ImagePath))
            //.ForMember(dest => dest.Product.Price, act => act.MapFrom(src => src.Price))
            //.ForMember(dest => dest.Tags, act => act.MapFrom(src => src.Tags))
            //.ForMember(dest => dest.Colors, act => act.MapFrom(src => src.Colors))
            //.ForMember(dest => dest.ImagesPath, act => act.MapFrom(src => src.ImagesPath));
            CreateMap<EditProductFieldRequest, UpdateProductCommand>();
            CreateMap<EditProductFieldRequest, Product>();
            //.ForMember(dest => dest.Product.id, act => act.MapFrom(src => src.id))
            //.ForMember(dest => dest.Product.Name, act => act.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Product.Slack, act => act.MapFrom(src => src.Slack))
            //.ForMember(dest => dest.Product.Number, act => act.MapFrom(src => src.Number))
            //.ForMember(dest => dest.Product.specs, act => act.MapFrom(src => src.specs))
            //.ForMember(dest => dest.Product.Brand, act => act.MapFrom(src => src.Brand))
            //.ForMember(dest => dest.Product.CategoryId, act => act.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.Product.Code, act => act.MapFrom(src => src.Code))
            //.ForMember(dest => dest.Product.Description, act => act.MapFrom(src => src.Description))
            //.ForMember(dest => dest.Product.Discount, act => act.MapFrom(src => src.Discount))
            //.ForMember(dest => dest.Product.ImagePath, act => act.MapFrom(src => src.ImagePath))
            //.ForMember(dest => dest.Product.Price, act => act.MapFrom(src => src.Price))
            //.ForMember(dest => dest.Tags, act => act.MapFrom(src => src.Tags))
            //.ForMember(dest => dest.Colors, act => act.MapFrom(src => src.Colors))
            //.ForMember(dest => dest.ImagesPath, act => act.MapFrom(src => src.ImagesPath)); 
        }
    }
}
