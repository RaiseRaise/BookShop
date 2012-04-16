using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BookShopModel.Model
{
    public class Sale
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " ")]
        [GreaterZero]
        public int Quantity { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Sale Date")]
        public DateTime Date { get; set; }

        public int BookId { get; set; }
        public int UserId { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
