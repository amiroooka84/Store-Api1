using MediatR;
using StoreApi.DAL.Repository.UserAddressRepository;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Command.DeleteUserAddress
{
    public class DeleteUserAddressQueryCommand : IRequestHandler<DeleteUserAddressCommand, Address>
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public DeleteUserAddressQueryCommand(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }
        public Task<Address> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            var res = _userAddressRepository.Delete(request.id);
            return Task.FromResult(res);
        }
    }
}
