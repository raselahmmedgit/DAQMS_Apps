﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAQMS.Domain.Models;

namespace DAQMS.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ContactJobTiteRatingStaffingRate>()
            //        .Map(e => e.ToTable("ContactJobTiteRatingStaffingRate"));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {

        }
    }

    #endregion
}
