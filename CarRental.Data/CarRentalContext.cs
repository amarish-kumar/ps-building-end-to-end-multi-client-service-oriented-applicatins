﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using CarRental.Business.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;
using Core.Common.Contracts;

namespace CarRental.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext():base("name=CarRental")
        {
            Database.SetInitializer<CarRentalContext>(null);
        }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<Car> CarSet { get; set; }
        public DbSet<Rental> RentalSet { get; set; }
        public DbSet<Reservation> ReservationSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // EF thinks that tables are pluralized in db
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // ignore the extension data object property
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();
            
            
            //base.OnModelCreating(modelBuilder);
        }
    }
}