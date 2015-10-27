using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Common.Core;

namespace CarRental.Client.Entities
{
    public class Car : TempObjectBase
    {
        private int _CarId;
        private string _Description;
        private string _Color;
        private decimal _RentalPrice;
        private bool _CurrentlyRented;

        public bool CurrentlyRented
        {
            get { return _CurrentlyRented; }
            set { _CurrentlyRented = value; }
        }


        public decimal RentalPrice
        {
            get { return _RentalPrice; }
            set { _RentalPrice = value; }
        }


        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }


        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        public int CarId
        {
            get { return _CarId; }
            set
            {
                if (_CarId != value)
                {
                    _CarId = value;

                    OnPropertyChanged("CarId");
                }
            }
        }

    }
}
