using StoreApi.Entity._User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Order
{
    public class Order
    {
        public int id { get; set; }

        public User User { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string Date { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int AmountPayment { get; set; }


        public enum state
        {
            Processing , Canceled, Delivered , WaitingPayment
        }
        public state State { get; set; }
        public int RefId { get; set; }
        public bool IsFinally { get; set; }
    }

    public class ProductOrder
    {
        public int id { get; set; }
        public Order Order{ get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public string ImagePath { get; set; }
    }
}
