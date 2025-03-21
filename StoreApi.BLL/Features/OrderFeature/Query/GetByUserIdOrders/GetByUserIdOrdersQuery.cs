using MediatR;
using StoreApi.Entity._Order;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Query.GetByUserIdOrders
{
    public class GetByUserIdOrdersQuery : IRequest<IEnumerable<Order>>
    {
        public string  UserId { get; set; }
    }
}
