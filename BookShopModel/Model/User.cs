using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BookShopModel.Model
{
    public class User
    {
        public int Id {get;set;}

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "!")]
        [Display(Name = "User Name")]
        public string UserName {get; set;}

        [Required(ErrorMessage = "*")]
        [StringLength(128, MinimumLength = 6, ErrorMessage = "Minium length = 6")]
        [DataType(DataType.Password, ErrorMessage = "!")]
        [Display(Name = "Password")]
        public string Password {get; set;}

        [Required(ErrorMessage = "*")]
        [StringLength(256, ErrorMessage = "!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "!")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$",
            ErrorMessage = "!")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
