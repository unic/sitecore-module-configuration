namespace Unic.Configuration.Converter
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts the field to an item.
    /// </summary>
    public class ItemConverter : AbstractConverter<Item>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The item of the field.</returns>
        public override Item Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            return string.IsNullOrWhiteSpace(field.Value) ? null : field.Item.Database.GetItem(field.Value);
        }
    }
}
