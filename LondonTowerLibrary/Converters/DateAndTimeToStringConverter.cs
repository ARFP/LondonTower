using System;
using System.Globalization;
using System.Windows.Data;

namespace LondonTowerLibrary.Converters
{
    /// <summary>
    /// Le converter transforme un type DateTime en un string formaté et vice-versa.     /// 
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateAndTimeToStringConverter : IValueConverter
    {
 
        public static DateAndTimeToStringConverter Instance = new DateAndTimeToStringConverter();
        /// <summary>
        /// transforme un type DateTime en un string formaté. En cas de date pa défaut, renvoit un string vide à la place.
        /// </summary>
        /// <param name="value">DateTime à convertir</param>
        /// <returns>string représentant la date formatée</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value != default) ? ((DateTime)value).ToString("dd/MM/yyyy HH:mm") : "";
        }
        /// <summary>
        /// transforme un string formaté en DateTime. En cas d'échec du parse, renvoit la valeur par défaut de DateTime.
        /// </summary>
        /// <param name="value">string formaté </param>
        /// <returns>DateTime résultant de la conversion</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime.TryParse((string)value, out DateTime date)) ? date : new DateTime();
        }
    }
}
