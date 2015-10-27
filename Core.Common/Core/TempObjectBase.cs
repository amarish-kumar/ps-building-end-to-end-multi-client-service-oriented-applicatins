using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace Core.Common.Core
{
    public class TempObjectBase : INotifyPropertyChanged
    {
        /// <summary>
        /// El evento del property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            if (makeDirty)
                _IsDirty = true;
        }

        bool _IsDirty;

        public bool IsDirty
        {
            get {
                return _IsDirty; 
            }
        }
    }
}
