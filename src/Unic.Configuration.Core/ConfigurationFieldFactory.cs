namespace Unic.Configuration.Core
{
    using Sitecore.Data.Items;

    /// <summary>
    /// The configuration field factory.
    /// </summary>
    public static class ConfigurationFieldFactory
    {
        /// <summary>
        /// Creates the configuration field.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// The configuration field.
        /// </returns>
        public static IConfigurationField Create(Item item, string fieldName)
        {
            var field = item.Fields[fieldName];
            if (field == null) return null;

            return new ConfigurationField
            {
                Item = item,
                Field = field,
                Name = field.Name,
                Value = field.Value
            };
        }
    }
}
