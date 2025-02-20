using MediatR;
using StoreApi.BLL.Features.CategoryFeature.Command.DeleteCategory;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Command.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var res = _categoryRepository.Update(request.Category);
            return Task.FromResult(res);
        }
    }
}
