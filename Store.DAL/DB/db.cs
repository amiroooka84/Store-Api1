using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StoreApi.Entity._User;
using StoreApi.Entity._Product;
using StoreApi.Entity._Category;
using StoreApi.Entity._Order;
using StoreApi.Entity._Basket;
using StoreApi.Entity._Image;
using StoreApi.Entity._Address;

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

            optionsBuilder.UseSqlServer(ConStr.con);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ImagePath> ImagesPath { get; set; }
  

    }
}
