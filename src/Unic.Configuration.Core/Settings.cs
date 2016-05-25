namespace Unic.Configuration.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// The configuration module settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The separators for multiple configuration values.
        /// </summary>
        private static readonly char[] Separators = { '|' };

        /// <summary>
        /// The config thunks.
        /// </summary>
        private static readonly Dictionary<string, Func<object>> ConfigThunks = new Dictionary<string, Func<object>>();

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Gets the root item template ids.
        /// </summary>
        /// <value>
        /// The root item template ids.
        /// </value>
        public IEnumerable<string> RootItemTemplateIds
        {
            get
            {
                var templateIds = ReadAppSettings("RootItem.TemplateIds", "{81C9B5BF-567C-4336-BB6C-3DF484031418}");
                return templateIds.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Gets the ruleset template id.
        /// </summary>
        /// <value>
        /// The ruleset template id.
        /// </value>
        public string RulesetTemplateId
        {
            get { return ReadAppSettings("Ruleset.TemplateId", "{ECD7572D-1361-414C-A5B6-D00D2C5BA9A3}"); }
        }

        /// <summary>
        /// Gets the ruleset container template id.
        /// </summary>
        /// <value>
        /// The ruleset container template id.
        /// </value>
        public string RulesetContainerTemplateId
        {
            get { return ReadAppSettings("RulesetContainer.TemplateId", "{FCC80608-BEC1-4683-A0DF-30070C2C6E5F}"); }
        }

        /// <summary>
        /// Gets the configuration template id.
        /// </summary>
        /// <value>
        /// The configuration template id.
        /// </value>
        public string ConfigurationTemplateId
        {
            get { return ReadAppSettings("Configuration.TemplateId", "{CF1CA8D4-A6D0-43D0-AFB2-AD7D72AB9997}"); }
        }

        /// <summary>
        /// Gets the custom root ruleset container item identifier.
        /// </summary>
        /// <value>
        /// The custom root ruleset container item identifier.
        /// </value>
        public string CustomRootRulesetContainerItemId
        {
            get { return ReadAppSettings("Configuration.CustomRootRulesetContainer.ItemId"); }
        }

        /// <summary>
        /// Reads the app settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The app setting for the key.
        /// </returns>
        public static string ReadAppSettings(string key, string defaultValue = "")
        {
            return ReadAppSettings(key, value => value) ?? defaultValue;
        }

        /// <summary>
        /// Reads the app settings.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueThunk">The value thunk.</param>
        /// <returns>The app settings for the key.</returns>
        public static T ReadAppSettings<T>(string key, Func<string, T> valueThunk)
        {
            // return fast if possible
            if (ConfigThunks.ContainsKey(key)) return (T)ConfigThunks[key]();

            // create the configuration in a safe manner
            lock (LockObject)
            {
                if (!ConfigThunks.ContainsKey(key))
                {
                    ConfigThunks.Add(
                        key,
                        () =>
                            {
                                // in order to support in-flight changes to config, these need to be Func<T>'s, not Lazy<T>'s, which will cache the value
                                // load from config
                                var keyName = string.Format(CultureInfo.InvariantCulture, "Config.{0}", key);
                                var value = Sitecore.Configuration.Settings.GetSetting(keyName);

                                // coalesce empty values to null
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    value = null;
                                }

                                // pass the value through the "thunk" which parses the string
                                return valueThunk(value);
                            });
                }
            }

            return (T)ConfigThunks[key]();
        }
    }
}