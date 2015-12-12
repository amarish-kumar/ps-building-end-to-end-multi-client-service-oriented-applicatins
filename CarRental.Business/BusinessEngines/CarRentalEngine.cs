using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Entities;

namespace CarRental.Business.BusinessEngines
{
    public class CarRentalEngine
    {
        public bool IsCarAvailableForRental(int carId, DateTime pickupDate, DateTime returnDate, IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars)
        {
            bool available = true;

            Reservation reservation = reservedCars.Where(item => item.CarId == carId).FirstOrDefault();
            if (reservation != null && (
                (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
                (pickupDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)    
            ))
            {
                available = true;
            }

            if (available)
            {
                Rental rental = rentedCars.Where(item => item.CarId == carId).FirstOrDefault();
                if (rental != null && (pickupDate <= rental.DateDue))
                    available = false;
            }

            return available;
        }
    }
}
