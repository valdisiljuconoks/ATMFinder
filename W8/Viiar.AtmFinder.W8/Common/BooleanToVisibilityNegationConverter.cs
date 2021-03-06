using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Viiar.AtmFinder.W8.Common
{
    public sealed class BooleanToVisibilityNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && !(bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Collapsed;
        }
    }
}
