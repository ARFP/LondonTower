using System;
using System.Globalization;
using System.Windows.Data;

namespace LondonTowerLibrary.Converters
{
    /// <summary>
    /// Le converter convertit une date arrivant sous format string en age et renvoit l'age sous forme de string. 
    ///  N'est pas censé être utilisé dans le sens IHM -> View Model
    /// </summary>
    [ValueConversion(typeof(String), typeof(string))]
    public class DateToAgeConverter : IValueConverter
    {
        public static DateToAgeConverter Instance = new DateToAgeConverter();
        /// <summary>
        /// Convertit une date arrivant sous format string en age et renvoit l'age sous forme de string. 
        /// </summary>
        /// <param name="value">string représentant une date</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var now = DateTime.Today;
            var date = (DateTime)value;
            var age = now.Year - date.Year;
                return ((date.Date>now.AddYears(-age))?--age:age).ToString();

        }
        /// <summary>
        /// Nécessaire pour implémenter l'interface mais non utilisé donc non implémenté
        /// </summary>
        /// <param name="value">string en age</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
