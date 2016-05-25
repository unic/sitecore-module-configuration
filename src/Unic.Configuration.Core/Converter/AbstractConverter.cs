namespace Unic.Configuration.Core.Converter
{
    using System;
    using Unic.Configuration.Core.Exceptions;

    /// <summary>
    /// The base class for the converters.
    /// </summary>
    /// <typeparam name="T">The conversion type.</typeparam>
    public abstract class AbstractConverter<T> : IConverter<T>
    {
        /// <summary>
        /// Gets the type of the conversion result.
        /// </summary>
        /// <returns>The type of the conversion result.</returns>
        public Type ConversionType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        object IConverter.Convert(IConfigurationField field)
        {
            try
            {
                return this.Convert(field);
            }
            catch (Exception ex)
            {
                throw new ConfigurationFieldConvertException(field, ex);
            }
        }

        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The converted value.</returns>
        public abstract T Convert(IConfigurationField field);
    }
}