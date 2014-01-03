using System;
using System.Collections.Generic;
using System.Linq;
using Viiar.AtmFinder.W8.Common;
using Windows.Storage;

namespace Viiar.AtmFinder.W8
{
    public class ApplicationSettings : BindableBase
    {
        private const string ItemsToShowKey = "ItemsToShowKey";
        private const string MyBanksKey = "MyBanksKey";
        private const string ShowCoordinatesKey = "ShowCoordinatesKey";
        private const string ShowMapOnStartupKey = "ShowMapOnStartupKey";
        private const string ShowOnlyMyBanksKey = "ShowOnlyMyBanksKey";
        private static ApplicationSettings instance;
        private string itemsToShow = "48";
        private bool show;
        private bool showMapOnStartup;
        private bool showOnlyMyBanks;

        public static ApplicationSettings Instance
        {
            get
            {
                return instance ?? (instance = new ApplicationSettings());
            }
        }

        public string[] ItemsToShowSelection
        {
            get
            {
                return new[] { "12", "24", "36", "48" };
            }
        }

        public string ItemsToShow
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(ItemsToShowKey))
                {
                    this.itemsToShow = ApplicationData.Current.LocalSettings.Values[ItemsToShowKey].ToString();
                }

                return this.itemsToShow;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[ItemsToShowKey] = value;
                if (value != null)
                {
                    SetProperty(ref this.itemsToShow, value);
                }
            }
        }

        public bool ShowOnlyMyBanks
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(ShowOnlyMyBanksKey))
                {
                    this.showOnlyMyBanks = Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values[ShowOnlyMyBanksKey].ToString());
                }

                return this.showOnlyMyBanks;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[ShowOnlyMyBanksKey] = value;
                SetProperty(ref this.showOnlyMyBanks, value);
            }
        }

        public bool ShowMapOnStartup
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(ShowMapOnStartupKey))
                {
                    this.showMapOnStartup = Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values[ShowMapOnStartupKey].ToString());
                }

                return this.showMapOnStartup;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[ShowMapOnStartupKey] = value;
                SetProperty(ref this.showMapOnStartup, value);
            }
        }

        public bool ShowCoordinates
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(ShowCoordinatesKey))
                {
                    this.show = Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values[ShowCoordinatesKey].ToString());
                }

                return this.show;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[ShowCoordinatesKey] = value;
                SetProperty(ref this.show, value);
            }
        }

        public List<string> MyBanks
        {
            get
            {
                if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(MyBanksKey))
                {
                    return new List<string>();
                }

                var val = ApplicationData.Current.LocalSettings.Values[MyBanksKey].ToString();
                return val.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[MyBanksKey] = string.Join(";", value);
            }
        }
    }
}
