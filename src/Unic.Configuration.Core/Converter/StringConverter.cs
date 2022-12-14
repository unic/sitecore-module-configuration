namespace Unic.Configuration.Core.Converter
{
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts the value of the configuration field to a string.
    /// </summary>
    public class StringConverter : AbstractConverter<string>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The string value of the configuration field.</returns>
        public override string Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            return field.Value;
        }
    }
}
