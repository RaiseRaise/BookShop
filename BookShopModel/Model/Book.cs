using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BookShopModel.Model
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "!")]
        [Display(Name = "Book")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = @"{0:F2}")]
        [GreaterZero]
        [Display(Name = "Book price")]
        public decimal Price { get; set; }
        public int AuthorId  { get; set; }
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual Author Author { get; set; }
    }
}
