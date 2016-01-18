using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using CarRental.Business.Entities;
using Core.Common.Exceptions;
using CarRental.Common;

namespace CarRental.Business.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IRentalService
    {
        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<Rental> GetRentalHistory(string loginEmail);
    }
}
