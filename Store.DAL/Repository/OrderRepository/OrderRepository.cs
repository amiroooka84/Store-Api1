using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.OrderRepository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(db db) : base(db)
        {

        }

    }
}
