namespace Unic.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Sitecore.Diagnostics;
    using Unic.Configuration.Converter;

    /// <summary>
    /// The base class for configuration module entities.
    /// </summary>
    public abstract class AbstractConfiguration : IConfiguration
    {
        /// <summary>
        /// The configuration fields.
        /// </summary>
        private readonly IDictionary<string, IConfigurationField> configurationFields = new Dictionary<string, IConfigurationField>();

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="configurationField">The configuration field.</param>
        /// <param name="converter">The converter.</param>
        public void AddValue(PropertyInfo propertyInfo, IConfigurationField configurationField, IConverter converter)
        {
            var key = this.GetStorageKey(propertyInfo);

            // add the property to the internal storage and set its value
            this.configurationFields.Add(key, configurationField);
            propertyInfo.SetValue(this, converter.Convert(configurationField), null);
        }

        /// <summary>
        /// Determines whether a property has a backing configuration field with a value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="func">The func.</param>
        /// <returns>
        ///   <c>true</c> if the field has a value; otherwise, <c>false</c>.
        /// </returns>
        public bool HasValueFor<TType, TValue>(Expression<Func<TType, TValue>> func)
        {
            Assert.ArgumentNotNull(func, "func");

            var expression = (MemberExpression)func.Body;
            var key = this.GetStorageKey(expression.Member);
            return this.configurationFields.ContainsKey(key) && this.configurationFields[key].HasValue;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The key.</returns>
        private string GetStorageKey(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}
