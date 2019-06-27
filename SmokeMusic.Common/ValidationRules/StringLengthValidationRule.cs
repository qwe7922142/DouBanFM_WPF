using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SmokeMusic.Common.ValidationRules
{
    public class StringLengthValidationRule : ValidationRule
    {
        public StringLengthValidationRule(int maxLength, int minLength)
            : this(maxLength)
        {
            this.MinLength = minLength;
        }
        public StringLengthValidationRule(int maxLength)
        {
            this.MaxLength = maxLength;
        }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
 
            }
            return new ValidationResult(true, null);
        }
    }
}
