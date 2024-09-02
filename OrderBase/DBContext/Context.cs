using Microsoft.EntityFrameworkCore;
using OrderBase.Entities;
using System.Xml;

namespace OrderBase.DBContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Customer>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderItem>().Property(e => e.Id).ValueGeneratedOnAdd();



        }
    }
}

