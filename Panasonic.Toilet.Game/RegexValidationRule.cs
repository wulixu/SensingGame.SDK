using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace TronCell.Game
{
    /// <summary>
    /// Validates a text against a regular expression
    /// </summary>
    public class RegexValidationRule : ValidationRule
    {
        private string _pattern;
        private Regex _regex;

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                _regex = new Regex(_pattern, RegexOptions.IgnoreCase);
            }
        }

        public RegexValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo ultureInfo)
        {
            if (value == null || !_regex.Match(value.ToString()).Success)
            {
                return new ValidationResult(false, "The value is not a valid e-mail address");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    [ValueConversion(typeof(ReadOnlyObservableCollection<ValidationError>), typeof(string))]
    public class ValidationErrorsToStringConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ValidationErrorsToStringConverter();
        }

        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            ReadOnlyObservableCollection<ValidationError> errors =
                value as ReadOnlyObservableCollection<ValidationError>;

            if (errors == null)
            {
                return string.Empty;
            }

            return string.Join("\n", (from e in errors
                                      select e.ErrorContent as string).ToArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
