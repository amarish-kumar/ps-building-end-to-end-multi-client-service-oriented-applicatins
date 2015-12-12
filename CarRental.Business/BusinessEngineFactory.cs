using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Common.Contracts;
using Core.Common.Core;

namespace CarRental.Business
{
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        public T GetBusinessEngine<T>() where T : IBusinessEngine
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
