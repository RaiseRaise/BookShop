using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BookShopModel.Model
{
    public class Theme
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "!")]
        [Display(Name = "Theme")]
        public string ThemeName { get; set; }
    }
}
