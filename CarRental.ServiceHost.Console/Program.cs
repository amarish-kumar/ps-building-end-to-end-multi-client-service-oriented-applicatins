using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using CarRental.Business.Bootstrapper;
using CarRental.Business.Entities;
using CarRental.Business.Managers;
using Core.Common;
using SM = System.ServiceModel;
using CarRental.Business.Managers.Managers;
using System.Transactions;

namespace CarRental.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up services...");
            Console.WriteLine("");


            SM.ServiceHost hostInventoryManager = new System.ServiceModel.ServiceHost(typeof(InventoryManager));
            SM.ServiceHost hostRentalManager = new System.ServiceModel.ServiceHost(typeof(RentalManager));
            SM.ServiceHost hostAccountManager = new System.ServiceModel.ServiceHost(typeof(AccountManager));

            StartService(hostInventoryManager, "InventoryManager");
            StartService(hostRentalManager, "RentalManager");
            StartService(hostAccountManager, "AccountManager");


            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Console.WriteLine("Reservation monitor started.");


            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();

            timer.Stop();
            Console.WriteLine("Reservation monitor stopped.");


            StopService(hostInventoryManager, "InventoryManager");
            StopService(hostRentalManager, "RentalManager");
            StopService(hostAccountManager, "AccountManager");
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RentalManager rentalManager = new RentalManager();
            Reservation[] reservations = rentalManager.GetDeadReservations();
            if (reservations != null)
            {
                foreach (Reservation  reservation in reservations)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            rentalManager.CancelReservation(reservation.ReservationId);
                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("There was an exception when attempting to cancel reservantion '{0}'", reservation.ReservationId);
                    }
                }
            }
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();

            Console.WriteLine("Service {0} stopped", serviceDescription);
        }



        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service {0} started.", serviceDescription);

            foreach (var endPoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoits:"));
                Console.WriteLine(string.Format("Address: {0}", endPoint.Address.Uri));
                Console.WriteLine(string.Format("Binding: {0}", endPoint.Binding.Name));
                Console.WriteLine(string.Format("Contract: {0}", endPoint.Contract.Name));
            }

            Console.WriteLine();
        }
    }
}
