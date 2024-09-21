using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StoreApi.Entity._User;
using StoreApi.Entity._Product;
using StoreApi.Entity._Category;
using StoreApi.Entity._Order;
using StoreApi.Entity._Basket;

namespace StoreApi.DAL.DB
{
    public class db : IdentityDbContext<User>
    {
        public db() : base() { }
        public db(DbContextOptions<db> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=StoreApi1;Integrated Security=True");
            //optionsBuilder.UseSqlServer("Server=.; Initial Catalog=stockss; User ID=stockss; Password=stockss.123; MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
    }
}
