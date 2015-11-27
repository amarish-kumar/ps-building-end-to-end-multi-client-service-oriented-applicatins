using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using CarRental.Business.Entities;

namespace CarRental.Business.Contracts
{
    [ServiceContract]
    public interface IInventoryService
    {
        [OperationContract]
        Car GetCar(int carId);

        [OperationContract]
        Car[] GetAllCars();
    }
}
