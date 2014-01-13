namespace Unic.Configuration
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    /// <summary>
    /// The configuration field represents a field in the Sitecore configuration
    /// item.
    /// </summary>
    public class ConfigurationField : IConfigurationField
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Item Item { get; set; }

        /// <summary>
        /// Gets or sets the Sitecore field.
        /// </summary>
        /// <value>
        /// The Sitecore field.
        /// </value>
        public Field Field { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the raw value.
        /// </summary>
        /// <value>
        /// The raw value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has a value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a value; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasValue
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Value);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Name: {0}, Value: {1}, Type: {2}", this.Name, this.Value, this.Field.Type);
        }
    }
}
