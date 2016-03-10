using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CarRental.Web.Core;
using System.ComponentModel.Composition;

namespace CarRental.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : ViewControllerBase
    {
        ISecurityAdapter _SecurityAdapter;

        [ImportingConstructor]
        public AccountController(ISecurityAdapter SecurityAdapter)
        {
            _SecurityAdapter = SecurityAdapter;
        }

        [HttpGet]
        [Route("login")]// acount/login
        public ActionResult Login()
        {
            _SecurityAdapter.Initialize();
            return View();
        }
    }
}