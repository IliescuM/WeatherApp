using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherAppMVVM.ViewModel.ValueConverters
{

    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRaining = (bool)value;

            return isRaining ? "Currently raining" : "Currently not raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRaining = (string)value;

            return isRaining == "Currently raining";
        }
    }
}
