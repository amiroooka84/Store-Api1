using MediatR;
using StoreApi.DAL.Repository.UserAddressRepository;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Query.GetUserAddresses
{
    public class GetUserAddressesQueryHandler : IRequestHandler<GetUserAddressesQuery, IEnumerable<Address>>
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public GetUserAddressesQueryHandler(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }
        public Task<IEnumerable<Address>> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
        {
            var res = _userAddressRepository.GetAddressesByUserId(request.UserId);
            return Task.FromResult(res);
        }
    }
}
