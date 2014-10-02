namespace Unic.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Sitecore.Data.Items;

    /// <summary>
    /// The configuration manager interface.
    /// </summary>
    public interface IConfigurationManager
    {      
        /// <summary>
        /// Gets the site root item.
        /// </summary>
        /// <value>
        /// The site root item.
        /// </value>
        Item SiteRoot { get; }

        /// <summary>
        /// Gets or sets a value indicating whether caching is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if caching is enabled; otherwise, <c>false</c>.
        /// </value>
        bool CachingEnabled { get; set; }

        /// <summary>
        /// Gets a value indicating whether it has active switchers.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has active switchers; otherwise, <c>false</c>.
        /// </value>
        bool HasActiveSwitchers { get; }

        /// <summary>
        /// Invalidates all configurations.
        /// </summary>
        void InvalidateConfigurations();

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the configuration value.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        TValue Get<TType, TValue>(Expression<Func<TType, TValue>> func) where TType : class;
    }
}
