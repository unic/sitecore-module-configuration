namespace Unic.Configuration.Abstraction
{
    using Sitecore.Data;

    public class SitecoreConfigurationContext : IConfigurationContext
    {
        /// <summary>
        /// Gets or sets the start path of a site.
        /// </summary>
        /// <value>
        /// The start path.
        /// </value>
        public string StartPath
        {
            get
            {
                return Sitecore.Context.Site.StartPath;
            }
        }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public Database Database
        {
            get
            {
                return Sitecore.Context.Database;
            }
        }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>
        /// The name of the site.
        /// </value>
        public string SiteName
        {
            get
            {
                return Sitecore.Context.Site.Name;
            }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language
        {
            get
            {
                return Sitecore.Context.Language.ToString();
            }
        }
    }
}
