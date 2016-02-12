using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using CarRental.Client.Entities;
using CarRental.Common;
using Core.Common.Exceptions;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Account GetCustomerAccountInfo(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateCustomerAccountInfo(Account account);

        [OperationContract]
        Task<Account> GetCustomerAccountInfoAsync(string loginEmail);

        [OperationContract]
        void UpdateCustomerAccountInfoAsync(Account account);
    }
}
