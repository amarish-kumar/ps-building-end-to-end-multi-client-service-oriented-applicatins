using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Common.Data;
using Core.Common.Contracts;

namespace CarRental.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, CarRentalContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
