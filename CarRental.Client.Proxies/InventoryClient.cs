using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarRental.Client.Contracts;
using System.ServiceModel;
using CarRental.Client.Entities;
using System.ComponentModel.Composition;
using Core.Common.ServiceModel;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {
        public void DeleteCar(int carId)
        {
            Channel.DeleteCar(carId);
        }

        public void DeleteCarAsync(int carId)
        {
            Channel.DeleteCarAsync(carId);
        }

        public Car[] GetAllCars()
        {
            return Channel.GetAllCars();
        }

        public Task<Car[]> GetAllCarsAsync()
        {
            return Channel.GetAllCarsAsync();
        }

        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCars(pickupDate, returnDate);
        }

        public Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCarsAsync(pickupDate, returnDate);
        }

        public Car GetCar(int carId)
        {
            return Channel.GetCar(carId);
        }

        public Task<Car> GetCarAsync(int carId)
        {
            return Channel.GetCarAsync(carId);
        }

        public Car Update(Car car)
        {
            return Channel.Update(car);
        }

        public Task<Car> UpdateAsync(Car car)
        {
            return Channel.UpdateAsync(car);
        }
    }
}
