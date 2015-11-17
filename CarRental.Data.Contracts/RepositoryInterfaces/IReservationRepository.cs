using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Entities;
using Core.Common.Contracts;

namespace CarRental.Data.Contracts.RepositoryInterfaces
{
    public interface IReservationRepository : IDataRepository<Reservation>
    {
        IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate);
        IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo();
        IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId);
    }
}
