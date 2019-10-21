using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LondonTowerLibrary.Converters
{
    [ValueConversion(typeof(Enum), typeof(Boolean))]
    public class EnumToBooleanConverter : IValueConverter
    {
        public static EnumToBooleanConverter Instance = new EnumToBooleanConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrEmpty((string)parameter) || Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;
            return (Enum.Parse(value.GetType(), (string)parameter)).Equals(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? parameter : Binding.DoNothing;
        }
    }
}
