using StoreApi.DAL.dl_Account;
using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Account
{
    public class bl_Account
    {
        dl_Account dl_Account = new dl_Account();
        public bool EditProfile(User user)
        {
            dl_Account dl_Account = new dl_Account();
            return dl_Account.EditProfile(user);
        }

        public bool ExsitUser(string? phoneNumber)
        {
            dl_Account dl_Account = new dl_Account();
            return dl_Account.ExsitUser(phoneNumber);
        }

        public bool UserVerification(string? phoneNumber, string? password)
        {
            dl_Account dl_Account = new dl_Account();
            return dl_Account.UserVerification(phoneNumber , password);
        }
    }
}
