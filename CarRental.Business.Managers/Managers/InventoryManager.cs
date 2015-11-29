using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Common.Contracts;
using Core.Common.Core;
using CarRental.Business.Contracts;
using System.ComponentModel.Composition;
using CarRental.Business.Entities;

namespace CarRental.Business.Managers
{
    public class InventoryManager : IInventoryService
    {
        public InventoryManager()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public Car GetCar(int carId)
        {
            throw new NotImplementedException();
        }

        public Car[] GetAllCars()
        {
            throw new NotImplementedException();
        }
    }
}
