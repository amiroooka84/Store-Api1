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
            CreateMap<ProductSpecsFieldRequest, ProductSpecs>();



            CreateMap<AddProductFieldRequest, AddProductCommand>();
            CreateMap<AddProductFieldRequest, Product>();               
            //.ForMember(dest => dest.Product.Name, act => act.MapFrom(src => src.Name))

            CreateMap<EditProductFieldRequest, UpdateProductCommand>();
            CreateMap<EditProductFieldRequest, Product>();
        }
    }
}
