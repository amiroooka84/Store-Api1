using MediatR;
using StoreApi.Entity._Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Command.ChangeOrderState
{
    public class ChangeOrderStateCommand : IRequest
    {
        public Order.state OrderState { get; set; }
        public int OrderId { get;  set; }
    }
}
