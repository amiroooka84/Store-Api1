using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.UserAddressRepository
{
    public class UserAddressRepository : RepositoryBase<Address>, IUserAddressRepository
    {
        public UserAddressRepository(db db) : base(db)
        {
        }

        public IEnumerable<Address> GetAddressesByUserId(string id)
        {
            _connection.Open();
            var res = _connection.Query<Address>("select * from Addresses where UserId = @ID" , new {ID = id});
            _connection.Close();
            return res;
        }
    }
}
