using MediatR;
using StoreApi.DAL.Repository.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Command.ChangeOrderState
{
    public class ChangeOrderStateCommandHandler : IRequestHandler<ChangeOrderStateCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeOrderStateCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<Unit> Handle(ChangeOrderStateCommand request, CancellationToken cancellationToken)
        {
           _orderRepository.ChangeOrderState(request.OrderId , request.OrderState);
            return Task.FromResult(Unit.Value);
        }
    }
}
