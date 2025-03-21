﻿using MediatR;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Query.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int OrderId { get; set; }
    }
}
