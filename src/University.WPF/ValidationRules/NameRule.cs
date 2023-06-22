using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace University.WPF.ValidationRules;

public class NameRule : ValidationRule
{
    public NameRule()
    {
    }

    public bool TextOnly { get; set; }

    public int Min { get; set; } = 1;
    
    public int Max { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var name = (string)value;
        
        if (name.Length < Min || name.Length > Max)
        {
            return new ValidationResult(false, $"The length of text must be in range: {Min}:{Max}");
        }

        if (TextOnly && !Regex.IsMatch(name, @"^[\p{L}\p{Mn}]+$"))
        {
            return new ValidationResult(false, $"The field is letter only.");
        }
        
        return ValidationResult.ValidResult;
    }
}