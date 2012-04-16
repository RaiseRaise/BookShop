using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopModel;
using BookShopModel.Interfaces;
using BookShopModel.Model;

namespace BookShopWeb.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        Repository repo;
        public HomeController(IModelContainer ModelContainer)
        {
            repo = new Repository(ModelContainer);
        }
        [HttpGet]
        public ActionResult Index()
        {
            User user = repo.GetUser(User.Identity.Name);
            if (user != null)
                ViewBag.SalesHistory = repo.GetSales(user).ToList();
            return View(repo.GetBooks().ToList());
        }
        [HttpGet]
        public ActionResult BuyBook(int id)
        {
            Sale newSale = new Sale { BookId = id, Book = repo.GetBookById(id), Quantity = 1, Date = DateTime.Now.Date };
            if (Request.IsAjaxRequest())
                return PartialView("BuyBookPartial", newSale);
            else
                return View(newSale);
        }
        [HttpPost]
        public ActionResult BuyBook(Sale newSale)
        {
            if (ModelState.IsValid)
            {
                if (repo.CreateSale(User.Identity.Name, newSale) > 0)
                    ViewBag.Message = "Thanks For buying";
                else
                    ModelState.AddModelError("", "Provider error");
            }
            newSale.Book = repo.GetBookById(newSale.BookId);
            if (Request.IsAjaxRequest())
                return PartialView("BuyBookPartial", newSale);
            else
                return View(newSale);
        }
    }
}
