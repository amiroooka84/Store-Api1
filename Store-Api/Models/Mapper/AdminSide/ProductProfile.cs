using AutoMapper;
using StoreApi.Entity._Product;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;

namespace StoreApi.Models.Mapper.AdminSide
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<EditProductFieldRequest, Product>();
            CreateMap<AddProductFieldRequest, Product>();
        }
    }
}
