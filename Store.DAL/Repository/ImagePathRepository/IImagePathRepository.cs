using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.ImagePathRepository
{
    public interface IImagePathRepository : IRepository<ImagePath>
    {
        IEnumerable<ImagePath> GetByProductId(int productId);
        void DeleteByProductId(int productId);
    }
}
