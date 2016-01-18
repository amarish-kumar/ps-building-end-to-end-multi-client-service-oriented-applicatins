using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;// es para la linea ObjectBase.Container.SatisfyImportsOnce(this);
using Core.Common.Core;
using System.ServiceModel;
using CarRental.Business.Entities;

namespace CarRental.Business.Managers
{
    public class ManagerBase
    {
        protected string _LoginName;
        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                // get the login from the header
                _LoginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");
                if (_LoginName.IndexOf(@"\") > 1)// comes from desk
                {
                    _LoginName = string.Empty;
                }
            }

            if(ObjectBase.Container != null)
            ObjectBase.Container.SatisfyImportsOnce(this);// resolve dependencies for this class after has been constructed

            if (!string.IsNullOrWhiteSpace(_LoginName))
                _AuthorizationAccount = LoadAuthorizationValidationAccount(_LoginName);
        }

        protected Account _AuthorizationAccount = null;
        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// void
        /// </summary>
        /// <param name="codeToExecute"></param>
        protected void ExecuteFaultHandledOperation(Action codeToExecute)
        {
            try
            {
                codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
