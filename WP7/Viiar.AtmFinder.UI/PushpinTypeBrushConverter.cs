using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Viiar.AtmFinder.UI
{
    /// <summary>
    ///   Data binding converter for converting a pushpin type to brush.
    /// </summary>
    public class PushpinTypeBrushConverter : IValueConverter
    {
        public static Brush DefaultBrush;

        static PushpinTypeBrushConverter()
        {
            DefaultBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x37, 0x84, 0xDF));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brushKey = string.Format("Pushpin{0}Brush", value);
            var brush = Application.Current.Resources[brushKey] ?? DefaultBrush;
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
