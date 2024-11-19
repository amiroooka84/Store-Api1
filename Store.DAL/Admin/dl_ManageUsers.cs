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
        public bool DeleteUser(string id)
        {
            db db = new db();
            foreach (var user in db.Users)
            {
                if (user.Id == id)
                {
                    db.Users.Remove(user);
                }
            }
            var res = db.SaveChanges();
            if (res == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            db db = new db();
            return db.Users.ToList();
        }
    }
}
