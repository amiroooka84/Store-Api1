using MediatR;
using StoreApi.Entity._Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.CategoryFeature.Query.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {

    }
}
