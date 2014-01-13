namespace Unic.Configuration
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Unic.Configuration.Converter;

    /// <summary>
    /// The interface for configuration module entities.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="configurationField">The configuration field.</param>
        /// <param name="converter">The converter.</param>
        void AddValue(PropertyInfo propertyInfo, IConfigurationField configurationField, IConverter converter);

        /// <summary>
        /// Determines whether a property has a backing configuration field with a value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="func">The func.</param>
        /// <returns>
        ///   <c>true</c> if the field has a value; otherwise, <c>false</c>.
        /// </returns>
        bool HasValueFor<TType, TValue>(Expression<Func<TType, TValue>> func);
    }
}