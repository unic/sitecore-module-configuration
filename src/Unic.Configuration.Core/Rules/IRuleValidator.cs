namespace Unic.Configuration.Core.Rules
{
    /// <summary>
    /// The rule validator interface.
    /// </summary>
    public interface IRuleValidator
    {
        /// <summary>
        /// Validates the specified rules.
        /// </summary>
        /// <param name="ruleList">The rule list.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        bool Validate(ConfigurationRuleList ruleList);
    }
}