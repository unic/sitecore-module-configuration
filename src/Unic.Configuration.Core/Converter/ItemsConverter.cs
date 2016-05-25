namespace Unic.Configuration.Core.Converter
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts a multilist field into an array of items.
    /// </summary>
    public class ItemsConverter : AbstractConverter<IEnumerable<Item>>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>An array of items from a multilist field.</returns>
        public override IEnumerable<Item> Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            return ((MultilistField)field.Field).GetItems() ?? Enumerable.Empty<Item>();
        }
    }
}
