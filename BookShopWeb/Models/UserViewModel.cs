using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.Models
{
    /// <summary>
    /// View Model пользователя при логине
    /// </summary>
    public class UserViewModel
    {
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "!")]
        [Display(Name = "User Name")]
        public string UserName {get; set;}

        [Required(ErrorMessage = "*")]
        [StringLength(128, MinimumLength = 6, ErrorMessage = "Minium length = 6")]
        [DataType(DataType.Password, ErrorMessage = "!")]
        [Display(Name = "Password")]
        public string Password {get; set;}
    }
}
