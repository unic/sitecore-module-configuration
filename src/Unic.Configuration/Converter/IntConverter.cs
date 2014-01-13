namespace Unic.Configuration.Converter
{
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts the field to an integer.
    /// </summary>
    public class IntConverter : AbstractConverter<int>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The integer value of the field.</returns>
        public override int Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            return int.Parse(field.Value);
        }
    }
}
