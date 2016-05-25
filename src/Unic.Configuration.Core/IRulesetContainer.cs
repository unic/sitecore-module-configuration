namespace Unic.Configuration.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// The ruleset container interface.
    /// </summary>
    public interface IRulesetContainer
    {
        /// <summary>
        /// Gets the ruleset key.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the configuration manager.
        /// </summary>
        /// <value>
        /// The configuration manager.
        /// </value>
        IConfigurationManager ConfigurationManager { get; }

        /// <summary>
        /// Gets the rulesets.
        /// </summary>
        /// <value>
        /// The rulesets.
        /// </value>
        IEnumerable<IRuleset> Rulesets { get; }

        /// <summary>
        /// Gets the fallback container.
        /// </summary>
        /// <value>
        /// The fallback container.
        /// </value>
        IRulesetContainer FallbackContainer { get; }
    }
}