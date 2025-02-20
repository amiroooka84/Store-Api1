using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.ProductColorsRepository
{
    public interface IProductColorsRepository : IRepository<ProductColors>
    {
        IEnumerable<ProductColors> GetByProductId(int productId);
        void DeleteByProductId(int productId);
    }
}
