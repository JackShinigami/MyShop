using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BUS_MyShop {
    public class EmptyInputRule : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {

            Debug.WriteLine("EmptyInputRule: Empty hit");
            if(value == null || value.ToString() == "" || string.IsNullOrWhiteSpace(value as string)) {
                return new ValidationResult(false, "Vui lòng nhập trường này");
            } 
            return ValidationResult.ValidResult;
        }
    }
}
