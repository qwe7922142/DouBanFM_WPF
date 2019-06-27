using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SmokeMusic.Common.ValidationRules
{
    public class RequiredValidationRule : ValidationBase
    {
        public RequiredValidationRule()
        {
            this.ErrorContent = "值不能为空!";
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, this.ErrorContent);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
