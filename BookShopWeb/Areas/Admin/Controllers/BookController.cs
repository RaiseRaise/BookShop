using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopModel;
using BookShopModel.Model;
using BookShopWeb.Models;
using BookShopModel.Interfaces;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class BookController : Controller
    {
        Repository repo;
        public BookController(IModelContainer ModelContainer)
        {
            repo = new Repository(ModelContainer);
        }

        public ActionResult Index()
        {
            return View(repo.GetBooks().ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Authors = repo.GetAuthors().AsSelectedList();
            ViewBag.Themes = repo.GetThemes().AsSelectedList();
            return View(new Book());
        }
        [HttpPost]
        public ActionResult Create(Book newBook)
        {
            if (ModelState.IsValid)
            {
                if (repo.CreateBook(newBook) > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Provider error");
            }
            ViewBag.Authors = repo.GetAuthors().AsSelectedList(newBook.AuthorId);
            ViewBag.Themes = repo.GetThemes().AsSelectedList(newBook.ThemeId);
            return View(newBook);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book editBook = repo.GetBookById(id);
            ViewBag.Authors = repo.GetAuthors().AsSelectedList(editBook.AuthorId);
            ViewBag.Themes = repo.GetThemes().AsSelectedList(editBook.ThemeId);
            return View(editBook);
        }
        [HttpPost]
        public ActionResult Edit(Book newBook)
        {
            if (ModelState.IsValid)
            {
                if (repo.EditBook(newBook) > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Provider error");
            }
            ViewBag.Authors = repo.GetAuthors().AsSelectedList(newBook.AuthorId);
            ViewBag.Themes = repo.GetThemes().AsSelectedList(newBook.ThemeId);
            return View(newBook);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repo.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
