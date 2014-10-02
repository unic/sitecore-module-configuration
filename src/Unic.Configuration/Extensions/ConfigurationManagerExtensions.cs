namespace Unic.Configuration.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Sitecore.Data.Items;

    /// <summary>
    /// The configuration manager extension methods.
    /// </summary>
    public static class ConfigurationManagerExtensions
    {
        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static string Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, string>> func) where TType : class
        {
            return configuration.Get(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static bool Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, bool>> func) where TType : class
        {
            return configuration.Get(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static int Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, int>> func) where TType : class
        {
            return configuration.Get(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static double Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, double>> func) where TType : class
        {
            return configuration.Get(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static DateTime Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, DateTime>> func) where TType : class
        {
            return configuration.Get(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static Item Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, Item>> func) where TType : class
        {
            return configuration.Get(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public static IEnumerable<Item> Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, IEnumerable<Item>>> func) where TType : class
        {
            return configuration.Get(func) ?? Enumerable.Empty<Item>();
        }

        /// <summary>
        /// Gets the specified configuration field.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration field.
        /// </returns>
        public static IConfigurationField Get<TType>(this IConfigurationManager configuration, Expression<Func<TType, IConfigurationField>> func) where TType : class
        {
            return configuration.Get(func);
        }
    }
}
