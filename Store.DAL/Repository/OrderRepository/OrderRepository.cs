using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace StoreApi.DAL.Repository.OrderRepository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(db db) : base(db)
        {

        }

        public void ChangeOrderState(int OrderId, Order.state OrderState)
        {
            _connection.Open();
            var res = _connection.QuerySingleOrDefault("update Orders SET State = "+ (int)OrderState +" WHERE id = @ID", new { ID = OrderId });
            _connection.Close();
        }

        public IEnumerable<Order> GetByUserIdOrders(string UserId)
        {
            _connection.Open();
            var res = _connection.Query<Order>("select * from Orders where user = @ID", new { ID = UserId});
            _connection.Close();
            return res;
        }

        public bool VerifyOrder(int OrderId)
        {
            _connection.Open();
            var res = _connection.QuerySingleOrDefault("update Orders SET IsFinally = 1 , State = 0  WHERE id = @ID", new { ID = OrderId });
            _connection.Close();
            return true;
        }
    }
}
