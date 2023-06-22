using System;
using System.Globalization;
using System.Windows.Controls;

namespace University.WPF.Validation;

public class NameRule : ValidationRule
{
    public NameRule()
    {
    }

    public int Min { get; set; } = 1;
    
    public int Max { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var name = (string)value;
        
        if (name.Length < Min || name.Length > Max)
        {
            return new ValidationResult(false, $"The length of text must be in range: {Min}:{Max}");
        }
        
        return ValidationResult.ValidResult;
    }
}