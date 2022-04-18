using System; 
using System.Globalization;
using System.Windows.Data;

namespace CustomerDetails.Converters
{
    public class CurrentAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dob = (DateTime) value;

            var today = DateTime.Today;

            var age = today.Year - dob.Year;

            if (dob > today.AddYears(-age))
            {
                age--;
            }

            return $"This persons age is {age}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
