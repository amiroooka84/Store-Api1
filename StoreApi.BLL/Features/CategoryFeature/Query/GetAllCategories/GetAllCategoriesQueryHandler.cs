using MediatR;
using StoreApi.BLL.Features.CategoryFeature.Command.UpdateCategory;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Query.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var res = _categoryRepository.GetAll();
            return Task.FromResult(res);
        }
    }
}
