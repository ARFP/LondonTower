using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LondonTowerLibrary.Converters
{
    [ValueConversion(typeof(String), typeof(string))]
    public class DateToAgeConverter : IValueConverter
    {
        public static DateToAgeConverter Instance = new DateToAgeConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var now = DateTime.Today;
            if (DateTime.TryParse((string)value, out DateTime date) && date < now )
            {
                var age = now.Year - date.Year;
                return ((date.Date>now.AddYears(-age))?--age:age).ToString();
            }
            else return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
