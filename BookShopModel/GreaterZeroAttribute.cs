using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BookShopModel
{
    /// <summary>
    /// Custom Validation Attribute
    /// </summary>
    public class GreaterZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Convert.ToDouble(value) > 0 ;
        }
    }
}
