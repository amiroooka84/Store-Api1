using StoreApi.DAL.Admin;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Admin
{
    public class bl_ManageUsers
    {
        public List<User> GetAllUsers()
        {
            dl_ManageUsers dl_ManageUsers = new dl_ManageUsers();
            return dl_ManageUsers.GetAllUsers();
        }
    }
}
