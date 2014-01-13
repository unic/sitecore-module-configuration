namespace Unic.Configuration.Exceptions
{
    using System;

    /// <summary>
    /// Custom converter exception.
    /// </summary>
    [Serializable]
    public class ConfigurationFieldConvertException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFieldConvertException"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConfigurationFieldConvertException(IConfigurationField field, Exception innerException)
            : base(BuildMessage(field), innerException)
        {
        }

        /// <summary>
        /// Builds the message.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>Error message</returns>
        private static string BuildMessage(IConfigurationField field)
        {
            return
                string.Format(
                    "Could not convert ConfigurationField value :: Name: '{0}', Value: '{1}', Item: '{2} ({3})'",
                    field.Name,
                    field.Value,
                    field.Item.Name,
                    field.Item.ID);
        }
    }
}
