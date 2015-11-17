using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Entities;
using Core.Common.Data;
using CarRental.Data.Contracts.RepositoryInterfaces;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Extensions;
using CarRental.Data.Contracts;

namespace CarRental.Data.DataRepositories
{
    [Export(typeof(ICarRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRepository : DataRepositoryBase<Car>, ICarRepository
    {
        protected override Car AddEntity(CarRentalContext entityContext, Car entity)
        {
            return entityContext.CarSet.Add(entity);
        }

        protected override IEnumerable<Car> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.CarSet
                   select e;
        }

        protected override Car GetEntity(CarRentalContext entityContext, int id)
        {
            var query = from e in entityContext.CarSet
                        where e.CarId == id
                        select e;

            var results = query.FirstOrDefault();

            return results;
        }

        protected override Car UpdateEntity(CarRentalContext entityContext, Car entity)
        {
            return (from e in entityContext.CarSet
                    where e.CarId == entity.CarId
                    select e).FirstOrDefault();
        }
    }
}
