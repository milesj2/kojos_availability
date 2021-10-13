using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KojosAvailability.Services
{
    public static class AppSettings
    {
        /// <summary>
        /// This is the Settings static class that can be used in your Core solution or in any
        /// of your client applications. All settings are laid out the same exact way with getters
        /// and setters. 
        /// </summary>

        private static ISettings Settings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string KEY_WATCHGUARD_URL = "watchguard_url";
        private const string KEY_WATCHGUARD_USERNAME = "watchguard_username";
        private const string KEY_WATCHGUARD_PASSWORD = "watchguard_password";
        private const string KEY_WATCHGUARD_ENABLED = "watchguard_enabled";

        private const string KEY_GARTAN_API_URL = "gartan_api_url";
        private const string KEY_GARTAN_API_KEY = "gartan_api_key";
        private const string KEY_GARTAN_USERNAME = "gartan_username";
        private const string KEY_GARTAN_PASSWORD = "gartan_password";
        private const string KEY_GARTAN_STATION = "gartan_station";

        private static readonly string SettingsDefaultString = string.Empty;

        #endregion

        public static void ResetSettings()
        {
            Settings.Clear();
        }

        public static bool NotSet(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool WatchGuardEnabled
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_WATCHGUARD_ENABLED, false);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_WATCHGUARD_ENABLED, value);
            }
        }

        public static string WatchGuardUrl
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_WATCHGUARD_URL, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_WATCHGUARD_URL, value);
            }
        }

        public static string WatchGuardUsername
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_WATCHGUARD_USERNAME, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_WATCHGUARD_USERNAME, value);
            }
        }

        public static string WatchGuardPassword
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_WATCHGUARD_PASSWORD, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_WATCHGUARD_PASSWORD, value);
            }
        }

        public static string GartanUsername
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_GARTAN_USERNAME, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_GARTAN_USERNAME, value);
            }
        }

        public static string GartanPassword
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_GARTAN_PASSWORD, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_GARTAN_PASSWORD, value);
            }
        }

        public static string GartanStation
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_GARTAN_STATION, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_GARTAN_STATION, value);
            }
        }



        public static string GartanApiUrl
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_GARTAN_API_URL, SettingsDefaultString);
            }

            set
            {
                Settings.AddOrUpdateValue(KEY_GARTAN_API_URL, value);
            }
        }

        public static string GartanApiKey
        {
            get
            {
                return Settings.GetValueOrDefault(KEY_GARTAN_API_KEY, SettingsDefaultString);
            }
            set
            {
                Settings.AddOrUpdateValue(KEY_GARTAN_API_KEY, value);
            }
        }

    }
}
