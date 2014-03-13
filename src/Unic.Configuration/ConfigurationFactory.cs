namespace Unic.Configuration
{
    using System;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Unic.Configuration.Converter;

    /// <summary>
    /// The configuration factory creates the configuration instance and tries
    /// to map its configuration fields to the proper values from the item.
    /// </summary>
    public static class ConfigurationFactory
    {
        /// <summary>
        /// Creates the specified full type. The type needs to have a constructor,
        /// which expects an item. Will fail otherwise.
        /// </summary>
        /// <param name="fullType">The full type.</param>
        /// <param name="item">The item.</param>
        /// <returns>The created configuration instance.</returns>
        public static IConfiguration Create(string fullType, Item item)
        {
            var type = Type.GetType(fullType);
            if (type == null) return null;

            var instance = Activator.CreateInstance(type);
            return GetConfiguration(instance, item);
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="item">The item.</param>
        /// <returns>The configuration object.</returns>
        private static IConfiguration GetConfiguration(object instance, Item item)
        {
            var configuration = instance as IConfiguration;
            if (configuration == null) return null;

            foreach (var propertyInfo in instance.GetType().GetProperties())
            {
                if (!propertyInfo.CanWrite) continue;

                // get the configuration attribute
                var attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationAttribute), true) as ConfigurationAttribute;
                if (attribute == null || string.IsNullOrWhiteSpace(attribute.FieldName)) continue;
                
                // get the configuration field from the item
                var configurationField = ConfigurationFieldFactory.Create(item, attribute.FieldName);
                if (configurationField == null || !configurationField.HasValue) continue;

                // get the converter based on the return type of the property
                var converter = ConverterFactory.Get(propertyInfo.PropertyType);

                // add the value to the configuration
                configuration.AddValue(propertyInfo, configurationField, converter);
            }

            return configuration;
        }
    }
}
