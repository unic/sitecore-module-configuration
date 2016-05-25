namespace Unic.Configuration.Core.Rules.Actions
{
    using Sitecore.Diagnostics;
    using Sitecore.Rules.Actions;

    /// <summary>
    /// Sets the isValid state for the configuration rule context.
    /// </summary>
    /// <typeparam name="T">The configuration rule context</typeparam>
    public class ConfigurationAction<T> : RuleAction<T> where T : ConfigurationRuleContext
    {
        /// <summary>
        /// Applies the specified rule context.
        /// </summary>
        /// <param name="ruleContext">The rule context.</param>
        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");

            ruleContext.IsValid = true;
        }
    }
}
