using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using CarRental.Business.Entities;
using Core.Common.Exceptions;

namespace CarRental.Business.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IRentalService
    {
        [OperationContract]
        IEnumerable<Rental> GetRentalHistory(string loginEmail);
    }
}
