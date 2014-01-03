using System;
using Viiar.AtmFinder.W8.Common;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Viiar.AtmFinder.W8.DataSources
{
    public class SearchMatch : BindableBase
    {
        private string country;
        private ImageSource mapImage;
        private string title;

        public string MapImageUri { get; set; }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                SetProperty(ref this.title, value);
            }
        }

        public string Country
        {
            get
            {
                return this.country;
            }

            set
            {
                SetProperty(ref this.country, value);
            }
        }

        public ImageSource Image
        {
            get
            {
                if (this.mapImage == null && MapImageUri != null)
                {
                    this.mapImage = new BitmapImage(new Uri(MapImageUri));
                }

                return this.mapImage;
            }

            set
            {
                MapImageUri = null;
                SetProperty(ref this.mapImage, value);
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
