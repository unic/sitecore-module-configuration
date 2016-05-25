namespace Unic.Configuration.Core.Converter
{
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts the field value to an double.
    /// </summary>
    public class DoubleConverter : AbstractConverter<double>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The double value of the field.</returns>
        public override double Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            return double.Parse(field.Value);
        }
    }
}
