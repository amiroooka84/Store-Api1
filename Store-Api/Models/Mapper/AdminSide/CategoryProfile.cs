using AutoMapper;
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
        }

    }
}
