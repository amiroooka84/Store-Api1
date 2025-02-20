using MediatR;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public  Task<Category> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var res = _categoryRepository.Delete(request.id);
            return Task.FromResult(res);
        }
    }
}
