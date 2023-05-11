using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.Validation
{
    public class MaxInt : ValidationRule
    {
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is int)
            {
                int NumberInt = (int)value;
                if (NumberInt > Max) return new ValidationResult(false, "Vrednost je veca od dozvoljene");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Nepoznata greska");
            }
        }
    }
}
