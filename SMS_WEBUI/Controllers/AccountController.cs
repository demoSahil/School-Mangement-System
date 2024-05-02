using SMS_BM;
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
        cls_User_BM _userBM;
        public AccountController()
        {
            _userBM = new cls_User_BM(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(cls_User_VO user)
        {
            if (_userBM.Authenticate(user))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                Session["Id"] = user.UserID.ToString();
                return RedirectToAction("Index", "Student");
            }
            string userType = user.UserType;
            switch (userType)
            {
                case string s when s.Contains("name"):
                    ModelState.AddModelError("UserName", user.ErrorMessage);
                    break;

                case string s when s.Contains("password"):
                    ModelState.AddModelError("Password", user.ErrorMessage);
                    break;

                case string s when s.Contains("Type"):
                    ModelState.AddModelError("UserType", user.ErrorMessage);
                    break;

            }
            return View();
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        public ActionResult SignUp()
        {
            return View( new cls_User_VO());
        }

        [HttpPost]
        public ActionResult SignUp(cls_User_VO newUser)
        {

            if (ModelState.IsValid && _userBM.Add(newUser))
            {
                TempData["_ToastMessage"] = "User Added Successfully";
                return RedirectToAction("Login");
            }

            return View();
        }




    }
}