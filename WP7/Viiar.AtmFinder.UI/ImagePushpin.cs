using System;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls.Maps;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.UI
{
    public class ImagePushpin
    {
        public ImagePushpin(Entity type)
        {
            Pushpin = new Pushpin();
            Image = new Image { Stretch = Stretch.None };

            // set current position pushpin
            if (type == null)
            {
                Pushpin.Background = new SolidColorBrush(Color.FromArgb(255, 0, 178, 90));
                Pushpin.Style = (Style)(Application.Current.Resources["PushpinStyle"]);
            }
            else
            {
                Id = type.Id;
                Location = new GeoCoordinate(type.Latitude, type.Longitude);
                Image.Source = new BitmapImage(new Uri(type.IconMap, UriKind.RelativeOrAbsolute));
            }
        }

        public Guid Id { get; set; }
        public Image Image { get; private set; }

        public GeoCoordinate Location
        {
            get { return Pushpin.Location; }
            set
            {
                Pushpin.Location = value;
                Image.SetValue(MapLayer.PositionProperty, value);
            }
        }

        public Pushpin Pushpin { get; private set; }
    }
}
