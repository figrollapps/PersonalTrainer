using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Figroll.PersonalTrainer.Converters
{
    public class BoolVisibilityConverter : IValueConverter
    {
        public Visibility IsTrue { get; set; }
        public Visibility IsFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool) value ? IsTrue : IsFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}