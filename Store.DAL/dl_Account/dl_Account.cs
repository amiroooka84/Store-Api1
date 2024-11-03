using StoreApi.DAL.DB;
using StoreApi.Entity._Address;
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
        public bool AddAddress(Address address)
        {
            db db = new db();
            db.Addresses.Add(address);
            db.SaveChanges();
            return true;
        }

        public bool DeleteAddress(Address address)
        {
            db db = new db();
            foreach (var item in db.Addresses)
            {
                if (item.id == address.id && item.UserId == address.UserId)
                {
                    db.Addresses.Remove(item);
                }
            }
            db.SaveChanges();
            return true;
        }

        public bool EditProfile(User user)
        {
            db db = new db();
            foreach (var item in db.Users)
            {
                if (user.PhoneNumber == item.PhoneNumber)
                {
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;

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

        public List<Address> GetAddresses(string id)
        {
            db db = new db();
            List<Address> addresses = new List<Address>();
            foreach(var item in db.Addresses)
            {
                if (item.UserId == id)
                {
                    addresses.Add(item);
                }
            }
            return addresses;
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
