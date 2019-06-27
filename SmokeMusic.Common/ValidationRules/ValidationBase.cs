using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SmokeMusic.Common.ValidationRules
{
    public abstract class ValidationBase : ValidationRule
    {
        public string ErrorContent { get; set; }
    }
}
