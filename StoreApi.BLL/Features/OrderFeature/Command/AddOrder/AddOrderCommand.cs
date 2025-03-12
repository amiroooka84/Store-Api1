using MediatR;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Command.AddOrder
{
    public class AddOrderCommand : IRequest<Order>
    {
        public OrderRequest Order { get; set; }
        public List<ProductOrderRequest> ProductsOrder { get; set; }
    }

    public class OrderRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string User { get; set; }
    }
    public class ProductOrderRequest
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Number { get; set; }
    }
}
