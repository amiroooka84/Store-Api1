using MediatR;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Query.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
}
