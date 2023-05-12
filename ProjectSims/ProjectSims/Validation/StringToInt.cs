using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.Validation
{
    public class StringToInt : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var NumberString = value as string;
                int NumberInt;
                if (int.TryParse(NumberString, out NumberInt))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Upisite ceo broj");
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greska");
            }
        }
    }
}
