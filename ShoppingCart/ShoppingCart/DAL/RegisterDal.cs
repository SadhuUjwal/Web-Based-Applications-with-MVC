using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.DAL
{
    public class RegisterDal:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerModel>().ToTable("tblcustomers");
        }
        public DbSet<CustomerModel> customers { get; set; }
    }
}