using CrudOperations.Interfaces;
using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Login = CrudOperations.Models.Login;

namespace CrudOperations.Controllers
{
    public class LoginController : Controller
    {
        readonly ILogin Logins;
        public LoginController(ILogin logins)
        {
            Logins = logins;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(LoginClass login , string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool data = await Logins.LoginAsync(login);
                if (data == true)
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else{return RedirectToAction("Index", "Catagory");}
                }
                else{ViewBag.LoginError = "Login Failed";}
            }
            return View();
        }
        public ActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        [ActionName("SignUp")]
        public async Task< ActionResult> SignUpAsync(SignUpClass signup)
        {
            if(ModelState.IsValid)
            {
                bool data = await Logins.InsertSignDetailsAsync(signup);

                if (data == true)
                {
                    ViewBag.LoginMessage = "Your Account has been created";
                    ModelState.Clear();
                }
                else{ViewBag.LoginMessage = "Your Account has not create";}
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}