using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using CarRental.Client.Entities;
using Core.Common.Exceptions;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IInventoryService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]// WCF necesita saber que puede retornar esto
        Car GetCar(int carId);

        [OperationContract]
        Car[] GetAllCars();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Car Update(Car car);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCar(int carId);

        [OperationContract]
        Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate);

        [OperationContract]
        Task<Car> UpdateAsync(Car car);

        [OperationContract]
        void DeleteCarAsync(int carId);

        [OperationContract]
        Task<Car> GetCarAsync(int carId);

        [OperationContract]
        Task<Car[]> GetAllCarsAsync();

        [OperationContract]
        Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate);
    }
}
