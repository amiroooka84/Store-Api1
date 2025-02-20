using Dapper;
using Microsoft.Data.SqlClient;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.ProductColorsRepository
{
    public class ProductColorsRepository : RepositoryBase<ProductColors>, IProductColorsRepository
    {
        public ProductColorsRepository(db db) : base(db)
        {
            
        }

        public void DeleteByProductId(int productId)
        {
            _connection.Open();
            _connection.Execute("delete from dbo.ProductColors where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
        }

        public IEnumerable<ProductColors> GetByProductId(int productId)
        {
            _connection.Open();
            var res = _connection.Query<ProductColors>("select * from ProductColors where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
            return res;
        }
    }
}
