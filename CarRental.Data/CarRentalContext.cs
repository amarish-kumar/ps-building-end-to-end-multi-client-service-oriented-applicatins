using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace CarRental.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext():base("name=CarRental")
        {
            Database.SetInitializer<CarRentalContext>(null);
        }
    }
}
