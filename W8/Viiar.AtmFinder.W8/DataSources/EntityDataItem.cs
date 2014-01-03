using System;
using Viiar.AtmFinder.W8.OnlineServices;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using EEEntityChainConstants = Viiar.AtmFinder.Core.Repositories.Estonia.EntityChainConstants;
using LTEntityChainConstants = Viiar.AtmFinder.Core.Repositories.Lithuania.EntityChainConstants;
using LVEntityChainConstants = Viiar.AtmFinder.Core.Repositories.Latvia.EntityChainConstants;

namespace Viiar.AtmFinder.W8.DataSources
{
    public class EntityDataItem : EntityDataCommon
    {
        private EntityDataGroup _group;
        private string chainName = string.Empty;
        private bool commissionWithdrawal;
        private string content = string.Empty;
        private string country;
        private double latitude;
        private ImageSource listImage;
        private string listImagePath;
        private double longitude;
        private ImageSource mapImage;
        private string mapImagePath;

        public EntityDataItem(
                string uniqueId,
                string title,
                string subtitle,
                string chain,
                string country,
                string description,
                string content,
                double latitude,
                double longitude,
                EntityDataGroup group) : base(uniqueId, title, subtitle, chain, description)
        {
            Country = country;
            this.chainName = ResolveChainName(chain);
            this._group = group;

            this.content = content;
            this.latitude = latitude;
            this.longitude = longitude;

            this.listImagePath = "Assets/Icons/" + chain + "-list.png";
            this.mapImagePath = "Assets/Icons/" + chain + "-map.png";
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

        public bool CommissionWithdrawal
        {
            get
            {
                return this.commissionWithdrawal;
            }

            set
            {
                SetProperty(ref this.commissionWithdrawal, value);
            }
        }

        public ImageSource ListImage
        {
            get
            {
                if (this.listImage == null && this.listImagePath != null)
                {
                    this.listImage = new BitmapImage(new Uri(ResourceBaseUri, this.listImagePath));
                }

                return this.listImage;
            }

            set
            {
                this.listImagePath = null;
                SetProperty(ref this.listImage, value);
            }
        }

        public ImageSource MapImage
        {
            get
            {
                if (this.mapImage == null && this.mapImagePath != null)
                {
                    this.mapImage = new BitmapImage(new Uri(ResourceBaseUri, this.mapImagePath));
                }

                return this.mapImage;
            }

            set
            {
                this.mapImagePath = null;
                SetProperty(ref this.mapImage, value);
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                SetProperty(ref this.content, value);
            }
        }

        public string ChainName
        {
            get
            {
                return this.chainName;
            }

            set
            {
                SetProperty(ref this.chainName, value);
            }
        }

        public EntityDataGroup Group
        {
            get
            {
                return this._group;
            }

            set
            {
                SetProperty(ref this._group, value);
            }
        }

        public double Latitude
        {
            get
            {
                return this.latitude;
            }

            set
            {
                SetProperty(ref this.latitude, value);
            }
        }

        public double Longitude
        {
            get
            {
                return this.longitude;
            }

            set
            {
                SetProperty(ref this.longitude, value);
            }
        }

        public CashDirection CashDirection { get; set; }
        public EntityType EntityType { get; set; }

        private string ResolveChainName(string chain)
        {
            // TODO: refactor this to retrieve from server instead
            if (Country.Equals("Latvia"))
            {
                return LVEntityChainConstants.GetName(chain);
            }

            if (Country.Equals("Estonia"))
            {
                return EEEntityChainConstants.GetName(chain);
            }

            return LTEntityChainConstants.GetName(chain);
        }
    }
}
