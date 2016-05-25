namespace Unic.Configuration.Core
{
    using System;

    /// <summary>
    /// The configuration attribute is used to map a configuration class to its
    /// sitecore entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConfigurationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the Sitecore item field.
        /// </summary>
        /// <value>
        /// The name of the Sitecore item field.
        /// </value>
        public string FieldName { get; set; }
    }
}
