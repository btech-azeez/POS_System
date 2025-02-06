using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace POS_System
{
    public class POSContext : DbContext
    {
        public POSContext() : base(ConfigurationManager.ConnectionStrings["POS_DBConnection"].ConnectionString)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
