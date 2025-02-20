using MediatR;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Query.GetByIdCategory
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<Category> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var res = _categoryRepository.GetById(request.id);
            return Task.FromResult(res);
        }
    }
}
