using MediatR;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.OrderRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Command.VerifyOrder
{
    public class VerifyOrderCommandHandler : IRequestHandler<VerifyOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public VerifyOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<bool> Handle(VerifyOrderCommand request, CancellationToken cancellationToken)
        {
            var res =   _orderRepository.VerifyOrder(request.OrderId);
            return Task.FromResult(res);
        }
    }
}
