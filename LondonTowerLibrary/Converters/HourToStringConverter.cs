using System;
using System.Globalization;
using System.Windows.Data;

namespace LondonTowerLibrary.Converters
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class HourToStringConverter : IValueConverter
    {
        public static HourToStringConverter Instance = new HourToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value != default) ? ((DateTime)value).ToString("HH:mm") : "";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime.TryParse((string)value, out DateTime date)) ? date : new DateTime();
        }
    }
}
