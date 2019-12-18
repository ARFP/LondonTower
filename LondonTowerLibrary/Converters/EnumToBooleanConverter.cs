using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LondonTowerLibrary.Converters
{
    /// <summary>
    /// Ce converter est utilisé pour faire la liaison entre un bouton radio (qui vaut true ou false) et un type enum. 
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(Boolean))]
    public class EnumToBooleanConverter : IValueConverter
    {
        public static EnumToBooleanConverter Instance = new EnumToBooleanConverter();
        /// <summary>
        /// Si la valeur de parameter(string) est non nulle, non vide et correspond à une valeur possible du type d'enum de value, retourne vrai si cette valeur est egale a l'enum value, false sinon
        /// si la valeur de parameter est nulle,  vide ou ne correspond à aucune valeur de l'enum ciblé, renvoit <c>DependencyProperty.UnsetValue</c> .      
        /// </summary>
        /// <param name="value">Enum </param>
        /// <param name="parameter"> La valeur passée en parametre de l'objet IHM (example, radio). string </param>
        /// <returns>Boolean </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrEmpty((string)parameter) || Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;
            return (Enum.Parse(value.GetType(), (string)parameter)).Equals(value);
        }
        /// <summary>
        /// retourne parameter si la valeur vaut true, et ne fait rien dans tout autre cas.
        /// </summary>
        /// <param name="value">boolean</param>
        /// <param name="parameter">string</param>
        /// <returns>string ou <c>Binding.DoNothing</c></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? parameter : Binding.DoNothing;
        }
    }
}
