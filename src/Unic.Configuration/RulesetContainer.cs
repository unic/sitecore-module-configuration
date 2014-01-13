namespace Unic.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Unic.Configuration.Extensions;

    /// <summary>
    /// The ruleset container representation class.
    /// </summary>
    public class RulesetContainer : IRulesetContainer
    {
        /// <summary>
        /// The underlying item.
        /// </summary>
        private readonly Item item;

        /// <summary>
        /// The settings.
        /// </summary>
        private readonly Settings settings;

        /// <summary>
        /// The rulesets.
        /// </summary>
        private IEnumerable<IRuleset> rulesets;

        /// <summary>
        /// The fallback ruleset container.
        /// </summary>
        private IRulesetContainer fallbackRulesetContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RulesetContainer" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="configurationManager">The configuration manager.</param>
        public RulesetContainer(string key, Item item, IConfigurationManager configurationManager)
            : this(key, item, configurationManager, TypeResolver.Settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RulesetContainer" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="settings">The settings.</param>
        public RulesetContainer(
            string key,
            Item item,
            IConfigurationManager configurationManager,
            Settings settings)
        {
            Assert.ArgumentNotNull(key, "key");
            Assert.ArgumentNotNull(item, "item");
            Assert.ArgumentNotNull(configurationManager, "configurationManager");
            Assert.ArgumentNotNull(settings, "settings");

            this.Key = key;
            this.item = item;
            this.ConfigurationManager = configurationManager;
            this.settings = settings;

            Assert.IsTrue(this.item.HasBaseTemplate(this.settings.RulesetContainerTemplateId), "Invalid item");
        }

        /// <summary>
        /// Gets the ruleset key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the configuration manager.
        /// </summary>
        /// <value>
        /// The configuration manager.
        /// </value>
        public IConfigurationManager ConfigurationManager { get; private set; }

        /// <summary>
        /// Gets the rulesets.
        /// </summary>
        /// <value>
        /// The rulesets.
        /// </value>
        public IEnumerable<IRuleset> Rulesets
        {
            get
            {
                return this.rulesets ?? (this.rulesets = this.GetRulesets().ToList());
            }
        }

        /// <summary>
        /// Gets the fallback container.
        /// </summary>
        /// <value>
        /// The fallback container.
        /// </value>
        public IRulesetContainer FallbackContainer
        {
            get
            {
                return this.fallbackRulesetContainer ?? (this.fallbackRulesetContainer = this.GetFallbackRulesetContainer());
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} Ruleset Container ({1})", this.item.Name, this.Key);
        }

        /// <summary>
        /// Gets the rulesets.
        /// </summary>
        /// <returns>
        /// The rulesets of the current item.
        /// </returns>
        protected virtual IEnumerable<IRuleset> GetRulesets()
        {
            Log.Debug("Loading new rulesets from item: " + this.item.Name, this);
            return ((MultilistField)this.item.Fields["Rulesets"]).GetItems().Select(rulesetItem => new Ruleset(rulesetItem, this.ConfigurationManager));
        }

        /// <summary>
        /// Gets the fallback ruleset container.
        /// </summary>
        /// <returns>The fallback ruleset container.</returns>
        protected virtual IRulesetContainer GetFallbackRulesetContainer()
        {
            var fallbackId = this.item["Fallback Container"];
            if (string.IsNullOrWhiteSpace(fallbackId)) return null;

            var fallbackItem = this.item.Database.GetItem(fallbackId);

            Log.Debug("Loading new fallback ruleset container: " + fallbackItem.Name, this);
            return new RulesetContainer(this.Key, fallbackItem, this.ConfigurationManager);
        }
    }
}
