using MediatR;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Command.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public  Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var res = _categoryRepository.Create(request.Category);
            return Task.FromResult(res);
        }
    }
}
