using MediatR;
using StoreApi.DAL.Repository.OrderRepository;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Query.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var res = _orderRepository.GetById(request.OrderId);
            return Task.FromResult(res);
        }
    }
}
