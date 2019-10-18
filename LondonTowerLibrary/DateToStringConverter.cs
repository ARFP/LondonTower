using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LondonTowerLibrary
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateToStringConverter : IValueConverter
    {
        public static DateToStringConverter Instance = new DateToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            if ( date != default )
            return date.ToString();
            return ""; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date= new DateTime();
            DateTime.TryParse((string)value, out date);
            return date;
        }
    }
}
