using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Contracts.ServiceContracts;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using System.ServiceModel;

namespace CarRental.Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RentalManager : ManagerBase, IRentalService
    {
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;
        [Import]
        IBusinessEngineFactory _businessEngineFactory;

        public RentalManager()
        {

        }

        public RentalManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public RentalManager(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        public RentalManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
            _businessEngineFactory = businessEngineFactory;
        }

        /*
        public RentalManager()
        {

        }
       */

        public IEnumerable<Rental> GetRentalHistory(string loginEmail)
        {
            throw new NotImplementedException();
        }
    }
}
