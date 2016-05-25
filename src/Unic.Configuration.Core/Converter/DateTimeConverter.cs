namespace Unic.Configuration.Core.Converter
{
    using System;
    using Sitecore;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Converts the field to a date.
    /// </summary>
    public class DateTimeConverter : AbstractConverter<DateTime>
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>Date of the field.</returns>
        public override DateTime Convert(IConfigurationField field)
        {
            Assert.ArgumentNotNull(field, "field");

            return DateUtil.IsoDateToDateTime(field.Value);
        }
    }
}
