using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Common.Contracts;
using Core.Common.Core;
using CarRental.Business.Contracts;
using System.ComponentModel.Composition;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.RepositoryInterfaces;
using Core.Common.Exceptions;
using System.ServiceModel;

namespace CarRental.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InventoryManager : ManagerBase, IInventoryService
    {
        public InventoryManager()
        {
            
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public Car GetCar(int carId)
        {
            /*try
            {*/
            return ExecuteFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _dataRepositoryFactory.GetDataRepository<ICarRepository>();
                Car carEntity = carRepository.Get(carId);

                if (carEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in the database", carId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return carEntity;
            });
            /*}
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            */
        }

        public Car[] GetAllCars()
        {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _dataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();

                IEnumerable<Car> cars = carRepository.Get();
                IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();

                foreach (Car car in cars)
                {
                    Rental rentedCar = rentedCars.Where(item => item.CarId == car.CarId).FirstOrDefault();
                    car.CurrentlyRented = (rentedCar != null);
                }

                return cars.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Car Update(Car car)
        {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _dataRepositoryFactory.GetDataRepository<ICarRepository>();

                Car updatedEntity = null;

                if (car.CarId == 0)
                    updatedEntity = carRepository.Add(car);
                else
                    updatedEntity = carRepository.Update(car);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCar(int carId)
        {
            ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _dataRepositoryFactory.GetDataRepository<ICarRepository>();

                carRepository.Remove(carId);
            });
        }

        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _dataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();
                IReservationRepository reservationRepository = _dataRepositoryFactory.GetDataRepository<IReservationRepository>();

                IEnumerable<Car> allCars = carRepository.Get();
                IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();
                IEnumerable<Reservation> reservedCars = reservationRepository.Get();

                List<Car> availableCars = new List<Car>();

                foreach (Car car in allCars)
                {
                    if (1 == 1)
                        availableCars.Add(car);
                }

                return availableCars.ToArray();
            });
        }
    }
}
