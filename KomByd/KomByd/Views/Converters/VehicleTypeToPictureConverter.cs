using System;
using System.Globalization;
using KomByd.Enums;
using Xamarin.Forms;

namespace KomByd.Views.Converters
{
    public class VehicleTypeToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is VehicleType))
            {
                return "vehicletype_bus_icon.png";
            }

            return (VehicleType) value == VehicleType.Bus ? "vehicletype_bus_icon.png" : "vehicletype_tram_icon.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
