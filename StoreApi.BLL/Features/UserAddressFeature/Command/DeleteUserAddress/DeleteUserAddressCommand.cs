using MediatR;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Command.DeleteUserAddress
{
    public class DeleteUserAddressCommand : IRequest<Address>
    {
        public int id { get; set; }
    }
}
