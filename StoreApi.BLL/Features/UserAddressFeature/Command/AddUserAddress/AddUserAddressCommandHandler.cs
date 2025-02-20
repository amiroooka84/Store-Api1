using MediatR;
using StoreApi.DAL.Repository.UserAddressRepository;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Command.AddUserAddress
{
    public class AddUserAddressCommandHandler : IRequestHandler<AddUserAddressCommand, Address>
    {  
        private readonly IUserAddressRepository _userAddressRepository;

        public AddUserAddressCommandHandler(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        public Task<Address> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            var res = _userAddressRepository.Create(request.Address);
            return Task.FromResult(res);
        }
    }
}
