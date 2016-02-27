using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.ComponentModel.Composition.Hosting;
using Core.Common.Extensions;

namespace CarRental.Web.Core
{
    public class MefDependencyResolver : IDependencyResolver
    {
        CompositionContainer _Container;
        public MefDependencyResolver(CompositionContainer container)
        {
            _Container = container;
        }

        public object GetService(Type serviceType)
        {
            return _Container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetExportedValuesByType(serviceType);
        }
    }
}