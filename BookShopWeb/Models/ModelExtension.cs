using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopModel.Model;

namespace BookShopWeb.Models
{
    public static class ModelExtension
    {
        /// <summary>
        /// Get Authors as SelectedList (for Views)
        /// </summary>
        /// <param name="Authors">List of Athors</param>
        /// <param name="currAuthor">Current Author will be selected</param>
        /// <returns></returns>
        public static List<SelectListItem> AsSelectedList(this IQueryable<Author> Authors, int currAuthor = 0)
        {
            return (from author in Authors.AsEnumerable()
                    select new SelectListItem
                    {
                        Value = author.Id.ToString(),
                        Text = author.FirstName + " " + author.LastName,
                        Selected = author.Id == currAuthor
                    }).ToList();
        }
        /// <summary>
        /// Get Themes as SelectedList (for Views)
        /// </summary>
        /// <param name="Themes">List of Themes</param>
        /// <param name="currTheme">Current Theme will be selected</param>
        /// <returns></returns>
        public static List<SelectListItem> AsSelectedList(this IQueryable<Theme> Themes, int currTheme = 0)
        {
            return (from theme in Themes.AsEnumerable()
                    select new SelectListItem
                    {
                        Value = theme.Id.ToString(),
                        Text = theme.ThemeName,
                        Selected = theme.Id == currTheme
                    }).ToList();
        }
        /// <summary>
        /// Make Reports A and B from Sales
        /// </summary>
        /// <param name="Sales">List of Sales</param>
        /// <param name="MinDate">Begin Date</param>
        /// <param name="MaxDate">End Date</param>
        /// <returns></returns>
        public static ReportViewModel AsReport(this IQueryable<Sale> Sales, DateTime MinDate, DateTime MaxDate)
        {
            List<Sale> filteredSales = Sales.Where(s => s.Date >= MinDate && s.Date <= MaxDate).ToList();
            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.TotalIncome = filteredSales.Sum(s => s.Quantity * s.Book.Price);
            reportViewModel.ReportA = (from author in filteredSales.GroupBy(e => new { e.Book.AuthorId, Author = e.Book.Author.FirstName + " " + e.Book.Author.LastName })
                                      select new ReportARecord
                                      {
                                          Author = author.Key.Author,
                                          Quantity = filteredSales.Where(s => s.Book.AuthorId == author.Key.AuthorId).Sum(e => e.Quantity)
                                      }).ToList();
            reportViewModel.ReportB = (from theme in filteredSales.GroupBy(e => new { e.Book.ThemeId, Theme = e.Book.Theme.ThemeName })
                                       select new ReportBRecord
                                       {
                                           Theme = theme.Key.Theme,
                                           Quantity = filteredSales.Where(s => s.Book.ThemeId == theme.Key.ThemeId).Sum(e => e.Quantity)
                                       }).ToList();
            return reportViewModel;
        }
    }
}
