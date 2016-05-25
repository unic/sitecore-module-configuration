namespace Unic.Configuration.Core
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    /// <summary>
    /// The configuration field interface.
    /// </summary>
    public interface IConfigurationField
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        Item Item { get; set; }

        /// <summary>
        /// Gets or sets the Sitecore field.
        /// </summary>
        /// <value>
        /// The Sitecore field.
        /// </value>
        Field Field { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the raw value.
        /// </summary>
        /// <value>
        /// The raw value.
        /// </value>
        string Value { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has a value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a value; otherwise, <c>false</c>.
        /// </value>
        bool HasValue { get; }
    }
}