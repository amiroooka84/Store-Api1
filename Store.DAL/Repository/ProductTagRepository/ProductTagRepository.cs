using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.ProductTagRepository
{
    public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagRepository
    {
        public ProductTagRepository(db db) : base(db)
        {
        }

        public void DeleteByProductId(int productId)
        {
            _connection.Open();
            _connection.Execute("delete from ProductTags where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
        }

        public IEnumerable<ProductTag> GetByProductId(int productId)
        {
            _connection.Open();
            var res = _connection.Query<ProductTag>("select * from ProductTags where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
            return res;
        }
    }
}
