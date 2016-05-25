namespace Unic.Configuration.Core
{
    /// <summary>
    /// The type resolver acts as a place to hold global instances of the configuration and
    /// the settings.
    /// </summary>
    public static class TypeResolver
    {
        /// <summary>
        /// The configuration managers.
        /// </summary>
        private static IConfigurationManager configuration;
        
        /// <summary>
        /// The settings.
        /// </summary>
        private static Settings settings;
        
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public static IConfigurationManager Configuration
        {
            get
            {
                return configuration ?? (configuration = new ConfigurationManager());
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public static Settings Settings
        {
            get
            {
                return settings ?? (settings = new Settings());
            }
        }
    }
}
