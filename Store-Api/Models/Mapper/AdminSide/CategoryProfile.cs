using AutoMapper;
using StoreApi.BLL.Features.CategoryFeature.Command.AddCategory;
using StoreApi.Entity._Category;
using StoreApi.Models.FieldsRequest.AdminSide.ManageCategory;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;

namespace StoreApi.Models.Mapper.AdminSide
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {   
            CreateMap<AddCategoryFieldRequest, Category>();
            CreateMap<EditCategoryFieldRequest, Category>();
            //CreateMap<EditCategoryFieldRequest, Category>().ForMember(dest => dest.Product, act => act.MapFrom(src => src));
        }

    }
}
