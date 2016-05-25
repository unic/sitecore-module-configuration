namespace Unic.Configuration.Core.Rules
{
    using Sitecore.Rules;
    using Unic.Configuration.Core.Rules.Actions;

    /// <summary>
    /// The configuration rule list.
    /// </summary>
    public class ConfigurationRuleList : RuleList<ConfigurationRuleContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRuleList" /> class.
        /// </summary>
        /// <param name="ruleList">The rule list.</param>
        public ConfigurationRuleList(RuleList<ConfigurationRuleContext> ruleList)
        {
            this.Name = ruleList.Name;
            this.AddRange(ruleList.Rules);
        }

        /// <summary>
        /// Runs the specified rule context.
        /// </summary>
        /// <param name="ruleContext">The rule context.</param>
        /// <param name="stopOnFirstMatching">if set to <c>true</c> stops on first matching.</param>
        /// <param name="executedRulesCount">The executed rules count.</param>
        protected override void Run(ConfigurationRuleContext ruleContext, bool stopOnFirstMatching, out int executedRulesCount)
        {
            this.ReplaceRuleActions();
            base.Run(ruleContext, stopOnFirstMatching, out executedRulesCount);
        }
        
        /// <summary>
        /// Replaces the rule actions.
        /// </summary>
        protected virtual void ReplaceRuleActions()
        {
            var action = this.GetAction();
            foreach (var rule in this.Rules)
            {
                rule.Actions.Clear();
                rule.Actions.Add(action);
            }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <returns>The action.</returns>
        protected virtual ConfigurationAction<ConfigurationRuleContext> GetAction()
        {
            return new ConfigurationAction<ConfigurationRuleContext>();
        }
    }
}
