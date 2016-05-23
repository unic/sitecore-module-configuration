namespace Unic.Configuration.Core.Rules
{
    using Sitecore.Rules;

    /// <summary>
    /// The configuration rule context.
    /// </summary>
    public class ConfigurationRuleContext : RuleContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }
    }
}
