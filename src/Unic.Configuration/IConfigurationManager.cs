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
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        string Get<TType>(Expression<Func<TType, string>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        bool Get<TType>(Expression<Func<TType, bool>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        int Get<TType>(Expression<Func<TType, int>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        double Get<TType>(Expression<Func<TType, double>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        DateTime Get<TType>(Expression<Func<TType, DateTime>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        Item Get<TType>(Expression<Func<TType, Item>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        IEnumerable<Item> Get<TType>(Expression<Func<TType, IEnumerable<Item>>> func) where TType : class;

        /// <summary>
        /// Gets the specified configuration field.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration field.
        /// </returns>
        IConfigurationField Get<TType>(Expression<Func<TType, IConfigurationField>> func) where TType : class;

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
