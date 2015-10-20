using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Car
    {
        private int _CarId;

        public int CarId
        {
            get { return _CarId; }
            set { _CarId = value; }
        }

    }
}
