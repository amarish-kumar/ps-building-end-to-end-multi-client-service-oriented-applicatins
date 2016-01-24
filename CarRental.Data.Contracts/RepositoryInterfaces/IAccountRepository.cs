﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Entities;
using Core.Common.Contracts;

namespace CarRental.Data.Contracts.RepositoryInterfaces
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string login);
    }
}
