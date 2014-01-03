using System;
using Viiar.AtmFinder.W8.Common;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Viiar.AtmFinder.W8.DataSources
{
    public class EntityDataCommon : BindableBase
    {
        public static readonly Uri ResourceBaseUri = new Uri("ms-appx:///");
        private string description = string.Empty;
        private ImageSource image;
        private string imagePath;
        private string subtitle = string.Empty;
        private string title = string.Empty;
        private string uniqueId = string.Empty;

        public EntityDataCommon(string uniqueId, string title, string subtitle, string chain, string description)
        {
            this.uniqueId = uniqueId;
            this.title = title;
            this.subtitle = subtitle;
            this.description = description;
            this.imagePath = "Assets/Icons/" + chain + "-details.png";
        }

        public string UniqueId { get { return this.uniqueId; } set { SetProperty(ref this.uniqueId, value); } }
        public string Title { get { return this.title; } set { SetProperty(ref this.title, value); } }
        public string Subtitle { get { return this.subtitle; } set { SetProperty(ref this.subtitle, value); } }
        public string Description { get { return this.description; } set { SetProperty(ref this.description, value); } }
        public string ImagePath { get { return this.imagePath; } }

        public ImageSource Image
        {
            get
            {
                if (this.image == null && this.imagePath != null)
                {
                    this.image = new BitmapImage(new Uri(ResourceBaseUri, this.imagePath));
                }

                return this.image;
            }

            set
            {
                this.imagePath = null;
                SetProperty(ref this.image, value);
            }
        }

        public override string ToString()
        {
            return Title;
        }

        public void SetImage(string path)
        {
            this.image = null;
            this.imagePath = path;
            OnPropertyChanged("Image");
        }
    }
}
