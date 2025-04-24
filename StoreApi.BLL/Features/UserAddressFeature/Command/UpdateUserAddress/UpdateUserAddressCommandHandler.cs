using MediatR;
using StoreApi.DAL.Repository.UserAddressRepository;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Command.UpdateUserAddress
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, Address>
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public UpdateUserAddressCommandHandler(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        public Task<Address> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            Address address = new Address()
            {
                id = request.id,
                PostCode = request.PostCode,
                _Address = request.Address,
            };
            var res = _userAddressRepository.Update(address);
            return Task.FromResult(res);
        }
    }
}
