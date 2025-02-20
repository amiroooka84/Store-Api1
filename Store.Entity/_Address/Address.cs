using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Address
{
    public class Address : EntityBase
    {
        public string _Address { get; set; }
        public string PostCode { get; set; }
        public string UserId { get; set; }
    }
}
