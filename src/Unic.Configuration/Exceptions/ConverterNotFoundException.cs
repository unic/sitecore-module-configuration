namespace Unic.Configuration.Exceptions
{
    using System;
    using Unic.Configuration.Converter;

    /// <summary>
    /// The converter could not be found exception.
    /// </summary>
    [Serializable]
    public class ConverterNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterNotFoundException" /> class.
        /// </summary>
        /// <param name="type">The type of the converter.</param>
        public ConverterNotFoundException(Type type) : base(BuildMessage(type))
        {
        }

        /// <summary>
        /// Builds the message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Error message
        /// </returns>
        private static string BuildMessage(Type type)
        {
            return string.Format("Could not find a converter for type '{0}'. Register a converter with '{1}'.", type, typeof(ConverterFactory));
        }
    }
}
