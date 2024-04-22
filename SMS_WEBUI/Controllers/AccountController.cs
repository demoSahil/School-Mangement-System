using SMS_DL;
using SMS_VO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SMS_WEBUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        cls_User_DL _userDL;
        public AccountController()
        {
            _userDL=new cls_User_DL(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(cls_User_VO user)
        {
            if(_userDL.AuthenticateUser(user))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Student");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        
        
    }
}