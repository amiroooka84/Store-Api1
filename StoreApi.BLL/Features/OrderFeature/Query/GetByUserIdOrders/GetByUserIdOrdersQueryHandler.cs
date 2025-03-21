using MediatR;
using StoreApi.DAL.Repository.OrderRepository;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Query.GetByUserIdOrders
{
    public class GetByUserIdOrdersQueryHandler : IRequestHandler<GetByUserIdOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetByUserIdOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<IEnumerable<Order>> Handle(GetByUserIdOrdersQuery request, CancellationToken cancellationToken)
        {
            var res = _orderRepository.GetByUserIdOrders(request.UserId);
            return Task.FromResult(res);
        }
    }
}
