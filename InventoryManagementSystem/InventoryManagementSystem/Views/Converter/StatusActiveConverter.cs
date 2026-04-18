using System;
using System.Globalization;
using System.Windows.Data;

namespace InventoryManagementSystem.Converters
{
    public class ActiveStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int status)
                return status == 1 ? "Active" : "Inactive";
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}