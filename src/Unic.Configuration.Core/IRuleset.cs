namespace Unic.Configuration.Core
{
    using System;
    using System.Collections.Generic;
    using Unic.Configuration.Core.Rules;

    /// <summary>
    /// The ruleset interface.
    /// </summary>
    public interface IRuleset
    {
        /// <summary>
        /// Gets the configuration manager.
        /// </summary>
        /// <value>
        /// The configuration manager.
        /// </value>
        IConfigurationManager ConfigurationManager { get; }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        ConfigurationRuleList Rules { get; }

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <value>
        /// The configurations.
        /// </value>
        IDictionary<Type, IConfiguration> Configurations { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }

        /// <summary>
        /// If the configurations are loaded and the collection does not contain a specific type of 
        /// configuration, the validation can be skipped. No value will be found.
        /// </summary>
        /// <typeparam name="TType">
        /// An implementation of a configuration.
        /// </typeparam>
        /// <returns>
        /// True, if validation can be skipped.
        /// </returns>
        bool CanSkipValidation<TType>();
    }
}