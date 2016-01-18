using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;// es para la linea ObjectBase.Container.SatisfyImportsOnce(this);
using Core.Common.Core;
using System.ServiceModel;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using System.Threading;
using CarRental.Common;

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

        protected void ValidateAuthorization(IAccountOwnedEntity entity)
        {
            if (!Thread.CurrentPrincipal.IsInRole(Security.CarRentalAdminRole))
            {
                if (_AuthorizationAccount != null)
                {
                    if (_LoginName != string.Empty && entity.OwnerAccountId != _AuthorizationAccount.AccountId)
                    {
                        AuthorizationValidationException ex = new AuthorizationValidationException("Attempt to access a secure");
                        throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                    }
                }
            }
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
