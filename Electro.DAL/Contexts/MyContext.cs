using Electro.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electro.DAL.Contexts
{
   
        public class MyContext : DbContext
        {
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasIndex(hi => hi.OrderNumber).IsUnique();
        }

    }
    
}
