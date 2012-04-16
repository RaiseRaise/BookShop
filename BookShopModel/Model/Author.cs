using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BookShopModel.Model
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
