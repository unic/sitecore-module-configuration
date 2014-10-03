namespace Unic.Configuration
{
    using Sitecore.Data;

    /// <summary>
    /// The configuration context. It's an abstraction of the Sitcore context.
    /// </summary>
    public interface IConfigurationContext
    {
        /// <summary>
        /// Gets or sets the start path of a site.
        /// </summary>
        /// <value>
        /// The start path.
        /// </value>
        string StartPath { get; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        Database Database { get; }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>
        /// The name of the site.
        /// </value>
        string SiteName { get; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        string Language { get; }
    }
}
