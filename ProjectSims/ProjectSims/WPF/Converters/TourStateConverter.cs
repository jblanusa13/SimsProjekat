using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ProjectSims.Domain.Model;

namespace ProjectSims.WPF.Converters
{
    public class TourStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if( value is TourState state)
                return Tour.GetState(state);
            return "<null>";
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Not in use
            return null;
        }
    }
}
