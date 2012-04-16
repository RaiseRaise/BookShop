using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookShopModel.Model;
using BookShopModel;
using BookShopWeb.Models;
using BookShopModel.Interfaces;

namespace BookShopWeb.Controllers
{
    public class AccountController : Controller
    {
        Repository repo;
        public AccountController(IModelContainer ModelContainer)
        {
            repo = new Repository(ModelContainer);
        }
        [HttpGet]
        public ActionResult LogOn()
        {
            return View(new UserViewModel());
        }
        [HttpPost]
        public ActionResult LogOn(UserViewModel LogonUser, string ReturnUrl)
        {
            string userName;
            if (ModelState.IsValid && repo.Authenticate(LogonUser.UserName, LogonUser.Password, out userName))
            {
                FormsAuthenticationService.SignIn(userName);
                if (String.IsNullOrEmpty(ReturnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(LogonUser);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Register(User RegisterUser)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus = repo.CreateUser(RegisterUser.UserName, RegisterUser.Password, RegisterUser.Email);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthenticationService.SignIn(RegisterUser.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            return View(RegisterUser);
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthenticationService.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
