using StoreApi.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.dl_Account
{
    public class dl_Account
    {
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
