using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Order;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.OrderRepository
{
    public class ProductOrderRepository : RepositoryBase<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(db db) : base(db)
        {
        }

        public IEnumerable<ProductOrder> GetProductsByOrderId(int orderId)
        {
            _connection.Open();
            var res = _connection.Query<ProductOrder>("select * from ProductOrders where OrderId = @ID", new { ID = orderId });
            _connection.Close();
            return res;
        }
    }
}
