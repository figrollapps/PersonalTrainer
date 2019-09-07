using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Figroll.PersonalTrainer.Converters
{
    public class NullOrEmptyVisibilityConverter : IValueConverter
    {
        public Visibility IsNull { get; set; }
        public Visibility IsEmpty { get; set; }
        public Visibility IsNotNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return IsNull;

            var valueString = value as string;
            if (valueString == null)
                return IsNull;

            return string.IsNullOrEmpty(valueString) ? IsEmpty : IsNotNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}