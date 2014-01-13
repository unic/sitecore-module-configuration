namespace Unic.Configuration.Rules
{
    using Sitecore.Data.Items;

    /// <summary>
    /// The rule validator can validate Sitecore rules. All conditions of all rules have to be 
    /// valid.
    /// </summary>
    public class RuleValidator : IRuleValidator
    {
        /// <summary>
        /// The item.
        /// </summary>
        private readonly Item item;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleValidator"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public RuleValidator(Item item)
        {
            this.item = item;
        }

        /// <summary>
        /// Validates the specified rules.
        /// </summary>
        /// <param name="ruleList">The rule list.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public bool Validate(ConfigurationRuleList ruleList)
        {
            // prepare the context
            var ruleContext = new ConfigurationRuleContext { Item = this.item };

            // run the evaluation
            ruleList.RunFirstMatching(ruleContext);

            // return the result from the context
            return ruleContext.IsValid;
        }
    }
}
