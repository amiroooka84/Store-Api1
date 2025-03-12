using MediatR;
using StoreApi.BLL.Service.ConvertDate;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.OrderRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.Entity._Order;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Command.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductColorsRepository _productColorsRepository;

        public AddOrderCommandHandler(IOrderRepository orderRepository, IProductOrderRepository productOrderRepository, IProductRepository productRepository, IProductColorsRepository productColorsRepository)
        {
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _productRepository = productRepository;
            _productColorsRepository = productColorsRepository;
        }

        public Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {

            int PriceOrder = 0;
            int DiscountOrder = 0;

            Order order = new Order()
            {
                User = request.Order.User,
                FullName = request.Order.FullName,
                PhoneNumber = request.Order.PhoneNumber,
                Address = request.Order.Address,
                Code = RandomNumberGenerator.GetInt32(100000, 999999),
                Price = PriceOrder,
                Discount = DiscountOrder,
                AmountPayment = PriceOrder - DiscountOrder,
                Date = ConvertDateTime.ConvertMiladiToShamsi(DateTime.Today, "yyyy/MM/dd"),
                IsFinally = false,
                RefId = 0,
                State = Order.state.WaitingPayment,
            };
            
            var res1 = _orderRepository.Create(order);

            ////////////////////////


            ////////////////////////

            foreach (var item in request.ProductsOrder)
            {
                ProductColors productColors = _productColorsRepository.GetById(item.ColorId);
                Product product = _productRepository.GetById(item.ProductId);
                ProductOrder productOrder = new ProductOrder()
                {
                    Color = productColors.Color,
                    Price = productColors.Price,
                    Discount = productColors.Discount,
                    Name = product.Name,
                    Number = item.Number,
                    ImagePath = product.ImagePath,
                    Code = product.Code,
                    OrderId = res1.id
                };
                PriceOrder += productOrder.Price;
                DiscountOrder += productOrder.Discount;


                _productOrderRepository.Create(productOrder);
            }

            ////////////////////
            res1.Price = PriceOrder;
            res1.Discount = DiscountOrder;
            res1.AmountPayment = PriceOrder - DiscountOrder;
            var res = _orderRepository.Update(res1);
            return Task.FromResult(res);
        
        }
    }
}
