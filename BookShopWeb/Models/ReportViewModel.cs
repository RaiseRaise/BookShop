using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.Models
{
    public struct ReportARecord
    {
        public string Author;
        public int Quantity;
    }
    public struct ReportBRecord
    {
        public string Theme;
        public int Quantity;
    }
    /// <summary>
    /// View Model отчета
    /// </summary>
    public class ReportViewModel
    {
        public List<ReportARecord> ReportA { get; set; }
        public List<ReportBRecord> ReportB { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Min Date")]
        public DateTime MinDate { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Max Date")]
        public DateTime MaxDate { get; set; }

        public decimal TotalIncome { get; set; }
    }
}