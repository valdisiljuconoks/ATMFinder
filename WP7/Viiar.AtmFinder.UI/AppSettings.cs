using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Extensions;
using Viiar.AtmFinder.Core.Repositories;
using Viiar.AtmFinder.UI.Repositories;

namespace Viiar.AtmFinder.UI
{
    public class AppSettings
    {
        #region Delegates

        public delegate void SettingsChangedEventHandler(object sender, SettingsChangeEventArgs e);

        #endregion

        public const string CountrySettingKeyName = "CountrySetting";
        public const string IsFirstLaunchSettingKeyName = "FirstLaunch";
        public const string LocationServicesSettingKeyName = "LocationServicesSetting";
        public const string OnlineServicesSettingKeyName = "OnlineServicesSetting";
        public const string MyChainNameSettingKeyName = "MyChainNameSetting";
        public const string ShowATMCashInSettingKeyName = "ShowATMCashInSetting";
        public const string ShowATMSettingKeyName = "ShowATMSetting";
        public const string ShowClosestSettingKeyName = "ShowClosestSetting";
        public const string ShowOfficesSettingKeyName = "ShowOfficesSetting";
        public const string ShowOnlyMyBankSettingKeyName = "ShowOnlyMyBankSetting";
        public const string ShowStoresSettingKeyName = "ShowStoresSetting";
        public const string UILanguageSettingKeyName = "UILanguageSetting";
        public const string ZoomToClosestSettingKeyName = "ZoomToClosestSetting";
        public const string ShowMapOnStartupSettingKeyName = "ShowMapOnStartupSetting";
        public const string ShowCoordinatesSettingKeyName = "ShowCoordinatesSetting";

        public const string AppVersionSettingKeyName = "AppVersion";

        private const string CountrySettingDefault = "Latvia";
        private const bool IsFirstLaunchSettingDefault = true;
        private const bool OnlineServicesSettingDefault = true;
        private const bool LocationServicesSettingDefault = true;
        private const string MyChainNameSettingDefault = "";
        private const bool ShowATMCashInSettingDefault = true;
        private const bool ShowATMSettingDefault = true;
        private const int ShowClosestSettingDefault = 10;
        private const bool ShowOfficesSettingDefault = true;
        private const bool ShowOnlyMyBankSettingDefault = false;
        private const bool ShowStoresSettingDefault = false;
        private const string UILanguageSettingDefault = "English";
        private const bool ZoomToClosestSettingDefault = true;
        private const bool ShowMapOnStartupSettingDefault = false;
        private const bool ShowCoordinatesSettingDefault = true;

        private static AppSettings instance;
        private static readonly object syncRoot = new object();

        private readonly ObservableCollection<string> chains =
            new ObservableCollection<string>(new List<string> { string.Empty });

        private readonly IsolatedStorageSettings settings;

        public AppSettings()
        {
        }

        private AppSettings(bool i)
        {
            this.settings = IsolatedStorageSettings.ApplicationSettings;
            RefreshChainList(CountrySetting);
        }

        public static AppSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new AppSettings(true);
                    }
                }

                return instance;
            }
        }

        public ObservableCollection<string> ChainsSelection
        {
            get { return this.chains; }
        }

        public string[] CountrySelection
        {
            get { return Settings.SupportedCountries.ToArray(); }
        }

        public string CountrySetting
        {
            get { return GetValueOrDefault(CountrySettingKeyName, CountrySettingDefault); }

            set
            {
                if (!AddOrUpdateValue(CountrySettingKeyName, value))
                {
                    return;
                }

                // TODO: extract this to some event handler not to couple settings class with reload fnc.
                CountryRepositoryFactory.SetByName(value);
                CashMachineRepository.Current.UnloadCurrentLists();

                Save(CountrySettingKeyName, value);

                //MyChainNameSetting = string.Empty;
                RefreshChainList(value);
            }
        }

        public bool IsFirstLaunchSetting
        {
            get { return GetValueOrDefault(IsFirstLaunchSettingKeyName, IsFirstLaunchSettingDefault); }

            set
            {
                if (AddOrUpdateValue(IsFirstLaunchSettingKeyName, value))
                {
                    Save(IsFirstLaunchSettingKeyName, value);
                }
            }
        }

        public bool IsOnlineServicesAvailable
        {
            get { return GetValueOrDefault(OnlineServicesSettingKeyName, OnlineServicesSettingDefault); }

            set
            {
                if (AddOrUpdateValue(OnlineServicesSettingKeyName, value))
                {
                    Save(OnlineServicesSettingKeyName, value);
                }
            }
        }

        public bool IsLocationServicesAvailable
        {
            get { return GetValueOrDefault(LocationServicesSettingKeyName, LocationServicesSettingDefault); }

            set
            {
                if (AddOrUpdateValue(LocationServicesSettingKeyName, value))
                {
                    Save(LocationServicesSettingKeyName, value);
                }
            }
        }

        public List<string> MyChainNameSetting
        {
            get
            {
                var val = GetValueOrDefault(MyChainNameSettingKeyName, MyChainNameSettingDefault);
                return val.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            set
            {
                var val = string.Join(";", value);
                if (AddOrUpdateValue(MyChainNameSettingKeyName, val))
                {
                    Save(MyChainNameSettingKeyName, val);
                }
            }
        }

        public bool ShowATMCashInSetting
        {
            get { return GetValueOrDefault(ShowATMCashInSettingKeyName, ShowATMCashInSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowATMCashInSettingKeyName, value))
                {
                    Save(ShowATMCashInSettingKeyName, value);
                }
            }
        }

        public bool ShowATMSetting
        {
            get { return GetValueOrDefault(ShowATMSettingKeyName, ShowATMSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowATMSettingKeyName, value))
                {
                    Save(ShowATMSettingKeyName, value);
                }
            }
        }

        public int[] ShowClosestSelections
        {
            get { return new[] { 1, 5, 10, 50 }; }
        }

        public int ShowClosestSetting
        {
            get { return GetValueOrDefault(ShowClosestSettingKeyName, ShowClosestSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowClosestSettingKeyName, value))
                {
                    Save(ShowClosestSettingKeyName, value);
                }
            }
        }

        public bool ShowOfficesSetting
        {
            get { return GetValueOrDefault(ShowOfficesSettingKeyName, ShowOfficesSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowOfficesSettingKeyName, value))
                {
                    Save(ShowOfficesSettingKeyName, value);
                }
            }
        }

        public bool ShowOnlyMyBankSetting
        {
            get { return GetValueOrDefault(ShowOnlyMyBankSettingKeyName, ShowOnlyMyBankSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowOnlyMyBankSettingKeyName, value))
                {
                    Save(ShowOnlyMyBankSettingKeyName, value);
                }
            }
        }

        public bool ShowStoresSetting
        {
            get { return GetValueOrDefault(ShowStoresSettingKeyName, ShowStoresSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowStoresSettingKeyName, value))
                {
                    Save(ShowStoresSettingKeyName, value);
                }
            }
        }

        public string[] UILanguageSelection
        {
            get
            {
                return new[]
                           {
                               CultureHelper.EnglishLanguage,
                               //CultureHelper.LatvianLanguage,
                               //CultureHelper.EstonianLanguage,
                               //CultureHelper.LithuanianLanguage,
                               //CultureHelper.RussianLanguage
                           };
            }
        }

        public string UILanguageSetting
        {
            get { return GetValueOrDefault(UILanguageSettingKeyName, UILanguageSettingDefault); }

            set
            {
                if (AddOrUpdateValue(UILanguageSettingKeyName, value))
                {
                    Save(UILanguageSettingKeyName, value, false);

                    // TODO: decouple
                    CultureHelper.SetResourceCulture(value);
                }
            }
        }

        public bool ZoomToClosestSetting
        {
            get { return GetValueOrDefault(ZoomToClosestSettingKeyName, ZoomToClosestSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ZoomToClosestSettingKeyName, value))
                {
                    Save(ZoomToClosestSettingKeyName, value, false);
                }
            }
        }

        public string AppVersion
        {
            get { return GetValueOrDefault(AppVersionSettingKeyName, "0.0"); }
            internal set
            {
                AddOrUpdateValue(AppVersionSettingKeyName, value);
                Save(AppVersionSettingKeyName, value);
            }
        }

        public bool ShowMapOnStartupSetting
        {
            get { return GetValueOrDefault(ShowMapOnStartupSettingKeyName, ShowMapOnStartupSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowMapOnStartupSettingKeyName, value))
                {
                    Save(ShowMapOnStartupSettingKeyName, value, false);
                }
            }
        }

        public bool ShowCoordinatesSetting
        {
            get { return GetValueOrDefault(ShowCoordinatesSettingKeyName, ShowCoordinatesSettingDefault); }

            set
            {
                if (AddOrUpdateValue(ShowCoordinatesSettingKeyName, value))
                {
                    Save(ShowCoordinatesSettingKeyName, value, false);
                }
            }
        }

        public static event SettingsChangedEventHandler SettingsChanged;

        protected void Save(string key, object value, bool triggerReload = true)
        {
            this.settings.Save();
            OnSettingsChanged(new SettingsChangeEventArgs(key, value, triggerReload));
        }

        protected bool AddOrUpdateValue(string key, object value)
        {
            var valueChanged = false;

            if (this.settings.Contains(key))
            {
                if (!this.settings[key].Equals(value))
                {
                    this.settings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                this.settings.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        protected T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (this.settings.Contains(key))
            {
                value = (T)this.settings[key];
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }

        private static void OnSettingsChanged(SettingsChangeEventArgs e)
        {
            var handler = SettingsChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        private void RefreshChainList(string country)
        {
            var repository = CountryRepositoryFactory.GetByName(country);
            for (var i = 1; i < this.chains.Count; i++)
            {
                this.chains.RemoveAt(1);
            }

            repository.RetrieveSupportedChains().ForEach(c => this.chains.Add(c));
        }

        internal void CheckFirstTimeLaunch()
        {
            var version = AppVersion;
            if (version.Equals("2.0"))
            {
                return;
            }

            ClearLocalSettings();
            IsFirstLaunchSetting = true;
            AppVersion = "2.0";
        }

        private void ClearLocalSettings()
        {
            var map = new Dictionary<string, object>
                          {
                              { CountrySettingKeyName, CountrySettingDefault },
                              { LocationServicesSettingKeyName, LocationServicesSettingDefault },
                              { MyChainNameSettingKeyName, MyChainNameSettingDefault },
                              { ShowATMCashInSettingKeyName, ShowATMCashInSettingDefault },
                              { ShowATMSettingKeyName, ShowATMSettingDefault },
                              { ShowClosestSettingKeyName, ShowClosestSettingDefault },
                              { ShowOfficesSettingKeyName, ShowOfficesSettingDefault },
                              { ShowOnlyMyBankSettingKeyName, ShowOnlyMyBankSettingDefault },
                              { ShowStoresSettingKeyName, ShowStoresSettingDefault },
                              { UILanguageSettingKeyName, UILanguageSettingDefault },
                              { ZoomToClosestSettingKeyName, ZoomToClosestSettingDefault },
                              { ShowMapOnStartupSettingKeyName, ShowMapOnStartupSettingDefault },
                              { ShowCoordinatesSettingKeyName, ShowCoordinatesSettingDefault }
                          };

            map.ForEach(m => AddOrUpdateValue(m.Key, m.Value));
        }
    }
}
