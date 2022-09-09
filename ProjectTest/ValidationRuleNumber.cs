using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace ProjectTest
{
    public class ValidationRuleNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse(value?.ToString(), out number))
                return new ValidationResult(false, "Недействительная запись");

            return ValidationResult.ValidResult;
        }
    }
}
