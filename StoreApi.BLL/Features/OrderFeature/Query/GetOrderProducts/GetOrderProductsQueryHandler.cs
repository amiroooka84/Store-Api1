using MediatR;
using StoreApi.DAL.Repository.OrderRepository;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Query.GetOrderProducts
{
    public class GetOrderProductsQueryHandler : IRequestHandler<GetOrderProductsQuery, IEnumerable<ProductOrder>>
    {
        private readonly IProductOrderRepository _productOrderRepository;

        public GetOrderProductsQueryHandler(IProductOrderRepository productOrderRepository)
        {
            _productOrderRepository = productOrderRepository;
        }

        public Task<IEnumerable<ProductOrder>> Handle(GetOrderProductsQuery request, CancellationToken cancellationToken)
        {
            var res = _productOrderRepository.GetProductsByOrderId(request.OrderId);
            return Task.FromResult(res);
        }
    }
}
