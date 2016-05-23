namespace Unic.Configuration.Core.Converter
{
    using Sitecore.Data.Fields;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts the field to a boolean value.
    /// </summary>
    public class BooleanConverter : AbstractConverter<bool>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>A boolean value of the field.</returns>
        public override bool Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            ReferenceField referenceField = field.Field;
            return referenceField.TargetItem != null && referenceField.TargetItem["Value"].Equals("1");
        }
    }
}
