using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucure.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        } 

        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductBrand>? ProductBrands { get; set; }
        public DbSet<ProductType>? ProductType { get; set; }
        public DbSet<OrderItem>? Orders { get; set; } // order  دي ف الداتا بيز 
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<DeliveryMethod>? DeliveryMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
