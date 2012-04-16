using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopModel;
using BookShopModel.Model;
using BookShopModel.Interfaces;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorController : Controller
    {
        Repository repo;
        public AuthorController(IModelContainer ModelContainer)
        {
            repo = new Repository(ModelContainer);
        }

        public ActionResult Index()
        {
            return View(repo.GetAuthors().ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Author());
        }
        [HttpPost]
        public ActionResult Create(Author newAuthor)
        {
            if (ModelState.IsValid)
            {
                if (repo.CreateAuthor(newAuthor) > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Provider error");
            }
            return View(newAuthor);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(repo.GetAuthorById(id));
        }
        [HttpPost]
        public ActionResult Edit(Author newAuthor)
        {
            if (ModelState.IsValid)
            {
                if (repo.EditAuthor(newAuthor) > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Provider error");
            }
            return View(newAuthor);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repo.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}
