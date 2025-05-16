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
    public class DeleteUserAddressCommandHandler : IRequestHandler<DeleteUserAddressCommand, Address>
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public DeleteUserAddressCommandHandler(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }
        public Task<Address> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            var Address = _userAddressRepository.GetById(request.id);
            Address res = null;
            if (Address.UserId == request.UserId)
            {
                 res = _userAddressRepository.Delete(request.id);
            }
            return Task.FromResult(res);
        }
    }
}
