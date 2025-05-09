﻿using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.OrderRepository
{
    public interface IProductOrderRepository : IRepository<ProductOrder>
    {
        public IEnumerable<ProductOrder> GetProductsByOrderId(int orderId);
    }
}
