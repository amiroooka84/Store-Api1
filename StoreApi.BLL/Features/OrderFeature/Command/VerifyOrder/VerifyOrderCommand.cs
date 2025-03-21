using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.OrderFeature.Command.VerifyOrder
{
    public class VerifyOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
    }
}
