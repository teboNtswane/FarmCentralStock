using FarmCentralStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FarmCentralStock.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (FCSDatabaseEntities context = new FCSDatabaseEntities())
            {
                bool IsValidUser = context.Users.Any(user => user.Username.ToLower() ==
                     model.Username.ToLower() && user.Password == model.Password);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (FCSDatabaseEntities context = new FCSDatabaseEntities())
            {
                if(context.Users.Any(user => user.Username.ToLower() ==
                     model.Username.ToLower()))
                {
                    ViewBag.Notification = "This account already exists.";
                    return View();
                }
                else
                {
                    context.Users.Add(model);
                    context.SaveChanges();
                }
                
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}