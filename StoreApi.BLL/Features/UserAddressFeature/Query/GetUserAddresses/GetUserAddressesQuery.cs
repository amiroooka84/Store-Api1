using MediatR;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Query.GetUserAddresses
{
    public class GetUserAddressesQuery : IRequest<IEnumerable<Address>>
    {
        public string UserId { get; set; } 
    }
}
