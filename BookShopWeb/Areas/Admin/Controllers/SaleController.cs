using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopModel;
using BookShopModel.Model;
using BookShopModel.Interfaces;
using BookShopWeb.Models;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SaleController : Controller
    {
        Repository repo;
        public SaleController(IModelContainer ModelContainer)
        {
            repo = new Repository(ModelContainer);
        }
        [HttpGet]
        public ActionResult GetReport()
        {
            return View(new ReportViewModel() { MinDate = DateTime.Now.Date, MaxDate = DateTime.Now.Date });
        }
        [HttpPost]
        public ActionResult GetReport(ReportViewModel report)
        {
            if (ModelState.IsValid)
                return View(repo.GetSales().AsReport(report.MinDate, report.MaxDate));
            else
                return View(report);
        }

    }
}
