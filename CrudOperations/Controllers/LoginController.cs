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

namespace CrudOperations.Controllers
{
    public class LoginController : Controller
    {
        readonly ICredential Logins;
        readonly IAuthenticationManager Authenticate;
        public LoginController(ICredential logins, IAuthenticationManager authenticate)
        {
            Logins = logins;
            Authenticate = authenticate;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(LoginDto login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //bool data = await Logins.LoginAsync(login);
                var data = await Logins.LoginEntAsync(login);
                if (data != null)
                {
                    //FormsAuthentication.SetAuthCookie(login.UserName, false);
                    //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                    var token = Authenticate.GenerateToken(data);

                    Response.Cookies.Set(new HttpCookie("Bearer", token));
                    return RedirectToAction("Index", "Catagory");
                }
                else { ViewBag.LoginError = "Logins Failed"; }
            }
            return View();
        }
    
 
        public ActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        [ActionName("SignUp")]
        public async Task< ActionResult> SignUpAsync(SignUpDto signup)
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
           var cookie = Request.Cookies["Bearer"];

            cookie.Expires = DateTime.Now.AddSeconds(1);

            Response.Cookies.Add(cookie);
            return View("Index");
        }
    }
}