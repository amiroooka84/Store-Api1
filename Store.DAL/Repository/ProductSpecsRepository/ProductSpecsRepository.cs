using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.ProductSpecsRepository
{
    public class ProductSpecsRepository : RepositoryBase<ProductSpecs>, IProductSpecsRepository
    {
        public ProductSpecsRepository(db db) : base(db)
        {
        }


        public void DeleteByProductId(int productId)
        {
            _connection.Open();
            _connection.Execute("delete from dbo.ProductSpecs where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
        }

        public IEnumerable<ProductSpecs> GetByProductId(int productId)
        {
            _connection.Open();
            var res = _connection.Query<ProductSpecs>("select * from ProductSpecs where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
            return res;
        }

    }
}
