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
    public class ThemeController : Controller
    {
        Repository repo;
        public ThemeController(IModelContainer ModelContainer)
        {
            repo = new Repository(ModelContainer);
        }

        public ActionResult Index()
        {
            return View(repo.GetThemes().ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Theme());
        }
        [HttpPost]
        public ActionResult Create(Theme newTheme)
        {
            if (ModelState.IsValid)
            {
                if (repo.CreateTheme(newTheme) > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Provider error");
            }
            return View(newTheme);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(repo.GetThemeById(id));
        }
        [HttpPost]
        public ActionResult Edit(Theme newTheme)
        {
            if (ModelState.IsValid)
            {
                if (repo.EditTheme(newTheme) > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Provider error");
            }
            return View(newTheme);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repo.DeleteTheme(id);
            return RedirectToAction("Index");
        }
    }
}
