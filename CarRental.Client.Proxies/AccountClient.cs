using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarRental.Client.Contracts;
using System.ServiceModel;
using CarRental.Client.Entities;

namespace CarRental.Client.Proxies
{
    public class AccountClient : ClientBase<IAccountService>, IAccountService
    {
        public Account GetCustomerAccountInfo(string loginEmail)
        {
            return Channel.GetCustomerAccountInfo(loginEmail);
        }

        public Task<Account> GetCustomerAccountInfoAsync(string loginEmail)
        {
            return Channel.GetCustomerAccountInfoAsync(loginEmail);
        }

        public void UpdateCustomerAccountInfo(Account account)
        {
            Channel.UpdateCustomerAccountInfo(account);
        }

        public void UpdateCustomerAccountInfoAsync(Account account)
        {
            Channel.UpdateCustomerAccountInfoAsync(account);
        }
    }
}
