namespace Unic.Configuration
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
        /// Gets the root item template ids.
        /// </summary>
        /// <value>
        /// The root item template ids.
        /// </value>
        public IEnumerable<string> RootItemTemplateIds
        {
            get
            {
                var templateIds = ReadAppSettings("RootItem.TemplateIds");
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
            get { return ReadAppSettings("Ruleset.TemplateId"); }
        }

        /// <summary>
        /// Gets the ruleset container template id.
        /// </summary>
        /// <value>
        /// The ruleset container template id.
        /// </value>
        public string RulesetContainerTemplateId
        {
            get { return ReadAppSettings("RulesetContainer.TemplateId"); }
        }

        /// <summary>
        /// Gets the configuration template id.
        /// </summary>
        /// <value>
        /// The configuration template id.
        /// </value>
        public string ConfigurationTemplateId
        {
            get { return ReadAppSettings("Configuration.TemplateId"); }
        }

        /// <summary>
        /// Gets the custom root ruleset container item identifier.
        /// </summary>
        /// <value>
        /// The custom root ruleset container item identifier.
        /// </value>
        public string CustomRootRulesetContainerItemId
        {
            get { return ReadAppSettings("Configuration.CustomRootRulesetContainerItemId"); }
        }

        /// <summary>
        /// Reads the app settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The app setting for the key.</returns>
        public static string ReadAppSettings(string key)
        {
            return ReadAppSettings(key, value => value);
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
            if (!ConfigThunks.ContainsKey(key))
            {
                ConfigThunks.Add(
                    key,
                    () =>
                    {
                        // in order to support in-flight changes to config, these need to be Func<T>'s, not Lazy<T>'s, which will cache the value
                        // load from config
                        var keyName = string.Format(CultureInfo.InvariantCulture, "Config.{0}", key);
                        var value = System.Configuration.ConfigurationManager.AppSettings[keyName];

                        // coalesce empty values to null
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            value = null;
                        }

                        // pass the value through the "thunk" which parses the string
                        return valueThunk(value);
                    });
            }

            return (T)ConfigThunks[key]();
        }
    }
}