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

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();

            StopService(hostInventoryManager, "InventoryManager");
            StopService(hostRentalManager, "RentalManager");
            StopService(hostAccountManager, "AccountManager");
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
