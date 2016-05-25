namespace Unic.Configuration.Core.Converter
{
    using System;

    /// <summary>
    /// The generic converter interface.
    /// </summary>
    /// <typeparam name="T">The conversion result type.</typeparam>
    public interface IConverter<out T> : IConverter
    {
        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The converted value.</returns>
        new T Convert(IConfigurationField field);
    }

    /// <summary>
    /// Defines the configuration field value converter interface.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Gets the type of the conversion result.
        /// </summary>
        /// <returns>The type of the conversion result.</returns>
        Type ConversionType { get; }

        /// <summary>
        /// Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The converted value.</returns>
        object Convert(IConfigurationField field);
    }
}
