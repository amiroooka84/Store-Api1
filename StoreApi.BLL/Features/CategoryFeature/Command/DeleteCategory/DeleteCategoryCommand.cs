using MediatR;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Command.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Category>
    {
        public int id { get; set; }
    }
}
