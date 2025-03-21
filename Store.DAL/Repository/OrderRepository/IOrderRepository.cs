using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Order;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.OrderRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public bool VerifyOrder(int OrderId);
        public IEnumerable<Order> GetByUserIdOrders(string UserId);
        public void ChangeOrderState(int OrderId , Order.state OrderState);
    }
}
