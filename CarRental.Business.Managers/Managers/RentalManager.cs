using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRental.Business.Contracts.ServiceContracts;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CarRental.Data.Contracts.RepositoryInterfaces;
using Core.Common.Exceptions;
using System.Security.Permissions;
using CarRental.Common;
using CarRental.Business.Contracts.DataContracts;
using CarRental.Data.Contracts;
using CarRental.Business.Common;

namespace CarRental.Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RentalManager : ManagerBase, IRentalService
    {
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;
        [Import]
        IBusinessEngineFactory _businessEngineFactory;

        public RentalManager()
        {

        }

        public RentalManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public RentalManager(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        public RentalManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
            _businessEngineFactory = businessEngineFactory;
        }

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
            Account authAcct = accountRepository.GetLogin(loginName);
            if (authAcct == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Cannot find account for login name {0} to use for security trimming", loginName));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return authAcct;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public IEnumerable<Rental> GetRentalHistory(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() => {
                IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();
                Account account = accountRepository.GetLogin(loginEmail);
                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

                IEnumerable<Rental> rentalHistory = rentalRepository.GetRentalHistoryByAccount(account.AccountId);

                return rentalHistory;

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void CancelReservation(int reservationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IReservationRepository reservationRepository = _dataRepositoryFactory.GetDataRepository<IReservationRepository>();

                Reservation reservation = reservationRepository.Get(reservationId);
                if (reservation == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No reservation found found for ID '{0}'.", reservationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                reservationRepository.Remove(reservationId);
            });
        }

        public CustomerReservationData[] GetCurrentReservations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IReservationRepository reservationRepository = _dataRepositoryFactory.GetDataRepository<IReservationRepository>();

                List<CustomerReservationData> reservationData = new List<CustomerReservationData>();

                IEnumerable<CustomerReservationInfo> reservationInfoSet = reservationRepository.GetCurrentCustomerReservationInfo();
                foreach (CustomerReservationInfo reservationInfo in reservationInfoSet)
                {
                    reservationData.Add(new CustomerReservationData()
                    {
                        ReservationId = reservationInfo.Reservation.ReservationId,
                        Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                        CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                        RentalDate = reservationInfo.Reservation.RentalDate,
                        ReturnDate = reservationInfo.Reservation.ReturnDate
                    });
                }

                return reservationData.ToArray();
            });
        }

        public CustomerReservationData[] GetCustomerReservations(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IReservationRepository reservationRepository = _dataRepositoryFactory.GetDataRepository<IReservationRepository>();

                Account account = accountRepository.GetLogin(loginEmail);
                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                List<CustomerReservationData> reservationData = new List<CustomerReservationData>();

                IEnumerable<CustomerReservationInfo> reservationInfoSet = reservationRepository.GetCustomerOpenReservationInfo(account.AccountId);
                foreach (CustomerReservationInfo reservationInfo in reservationInfoSet)
                {
                    reservationData.Add(new CustomerReservationData()
                    {
                        ReservationId = reservationInfo.Reservation.ReservationId,
                        Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                        CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                        RentalDate = reservationInfo.Reservation.RentalDate,
                        ReturnDate = reservationInfo.Reservation.ReturnDate
                    });
                }

                return reservationData.ToArray();
            });
        }

        public Rental GetRental(int rentalId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();

                Rental rental = rentalRepository.Get(rentalId);
                if (rental == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No rental record found for id '{0}'.", rentalId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return rental;
            });
        }

        public CustomerRentalData[] GetCurrentRentals()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();

                List<CustomerRentalData> rentalData = new List<CustomerRentalData>();

                IEnumerable<CustomerRentalInfo> rentalInfoSet = rentalRepository.GetCurrentCustomerRentalInfo();
                foreach (CustomerRentalInfo rentalInfo in rentalInfoSet)
                {
                    rentalData.Add(new CustomerRentalData()
                    {
                        RentalId = rentalInfo.Rental.RentalId,
                        Car = rentalInfo.Car.Color + " " + rentalInfo.Car.Year + " " + rentalInfo.Car.Description,
                        CustomerName = rentalInfo.Customer.FirstName + " " + rentalInfo.Customer.LastName,
                        DateRented = rentalInfo.Rental.DateRented,
                        ExpectedReturn = rentalInfo.Rental.DateDue
                    });
                }

                return rentalData.ToArray();
            });
        }

        public Reservation[] GetDeadReservations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IReservationRepository reservationRepository = _dataRepositoryFactory.GetDataRepository<IReservationRepository>();

                IEnumerable<Reservation> reservations = reservationRepository.GetReservationsByPickupDate(DateTime.Now.AddDays(-1));

                return (reservations != null ? reservations.ToArray() : null);
            });
        }

        public bool IsCarCurrentlyRented(int carId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICarRentalEngine carRentalEngine = _businessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                return carRentalEngine.IsCarCurrentlyRented(carId);
            });
        }
    }
}
