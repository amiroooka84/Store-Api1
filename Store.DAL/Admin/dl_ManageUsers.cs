using StoreApi.DAL.DB;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Admin
{
    public class dl_ManageUsers
    {
        public List<User> GetAllUsers()
        {
            db db = new db();
            return db.Users.ToList();
        }
    }
}
