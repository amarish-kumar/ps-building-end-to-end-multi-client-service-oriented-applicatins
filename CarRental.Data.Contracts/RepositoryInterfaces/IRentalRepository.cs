using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Entities;
using Core.Common.Contracts;

namespace CarRental.Data.Contracts.RepositoryInterfaces
{
    public interface IRentalRepository : IDataRepository<Rental>
    {
        IEnumerable<Rental> GetRentalHistoryByCar(int carId);
        Rental GetCurrentRentalByCar(int carId);
        IEnumerable<Rental> GetCurrentlyRentedCars();
        IEnumerable<Rental> GetRentalHistoryByAccount(int accountId);
        IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
    }
}
