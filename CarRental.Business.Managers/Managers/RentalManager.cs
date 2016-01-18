using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Contracts.ServiceContracts;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CarRental.Data.Contracts.RepositoryInterfaces;
using Core.Common.Exceptions;
using System.Security.Permissions;
using CarRental.Common;

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

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
            Account authAcct = accountRepository.GetLogin(loginName);
            if (authAcct == null)
                throw new NotFoundException(string.Format("Cannot find account for login name {0} to use for security trimming", loginName));

            return authAcct;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public IEnumerable<Rental> GetRentalHistory(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() => {
                IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();
                Account account = accountRepository.GetLogin(loginEmail);
                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

                IEnumerable<Rental> rentalHistory = rentalRepository.GetRentalHistoryByAccount(account.AccountId);

                return rentalHistory;

            });
        }
    }
}
