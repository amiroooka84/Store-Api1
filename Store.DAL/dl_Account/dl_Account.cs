using StoreApi.DAL.DB;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.dl_Account
{
    public class dl_Account
    {

        public bool EditProfile(User user)
        {
            db db = new db();
            foreach (var item in db.Users)
            {
                if (user.PhoneNumber == item.PhoneNumber)
                {
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Address = user.Address;
                    item.PostCode = user.PostCode;
                }
            }
            db.SaveChanges();
            return true;
        }

        public bool ExsitUser(string phoneNumber)
        {
            db db = new db();
            foreach (var user in db.Users) 
            {
                if (user.PhoneNumber == phoneNumber)
                {
                    return true;
                }          
            }
            return false;
        }


        public bool UserVerification(string phoneNumber, string password)
        {
            db db = new db();
            foreach (var user in db.Users)
            {
                if (user.PhoneNumber == phoneNumber && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
