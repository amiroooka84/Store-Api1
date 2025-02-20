using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.UserAddressRepository
{
    public interface IUserAddressRepository : IRepository<Address>
    {
        IEnumerable<Address> GetAddressesByUserId(string id);
    }
}
