namespace Unic.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Rules;
    using Unic.Configuration.Extensions;
    using Unic.Configuration.Rules;

    /// <summary>
    /// The ruleset representation class.
    /// </summary>
    public class Ruleset : IRuleset
    {
        /// <summary>
        /// The underlying item.
        /// </summary>
        private readonly Item item;

        /// <summary>
        /// The rule validator.
        /// </summary>
        private readonly IRuleValidator ruleValidator;

        /// <summary>
        /// The settings.
        /// </summary>
        private readonly Settings settings;

        /// <summary>
        /// The rules of the ruleset.
        /// </summary>
        private ConfigurationRuleList rules;

        /// <summary>
        /// The configurations.
        /// </summary>
        private IDictionary<Type, IConfiguration> configurations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ruleset" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="configurationManager">The configuration manager.</param>
        public Ruleset(Item item, IConfigurationManager configurationManager)
            : this(
            item,
            configurationManager,
            new RuleValidator(item),
            TypeResolver.Settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ruleset" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="ruleValidator">The rule validator.</param>
        /// <param name="settings">The settings.</param>
        public Ruleset(
            Item item,
            IConfigurationManager configurationManager,
            IRuleValidator ruleValidator,
            Settings settings)
        {
            Assert.ArgumentNotNull(item, "item");
            Assert.ArgumentNotNull(configurationManager, "configurationManager");
            Assert.ArgumentNotNull(ruleValidator, "ruleValidator");
            Assert.ArgumentNotNull(settings, "settings");

            this.item = item;
            this.ConfigurationManager = configurationManager;
            this.ruleValidator = ruleValidator;
            this.settings = settings;

            Assert.IsTrue(this.item.HasBaseTemplate(this.settings.RulesetTemplateId), "Invalid item");
        }

        /// <summary>
        /// Gets the configuration manager.
        /// </summary>
        /// <value>
        /// The configuration manager.
        /// </value>
        public IConfigurationManager ConfigurationManager { get; private set; }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        public ConfigurationRuleList Rules
        {
            get
            {
                return this.rules ?? (this.rules = this.GetRules());
            }
        }

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <value>
        /// The configurations.
        /// </value>
        public IDictionary<Type, IConfiguration> Configurations
        {
            get
            {
                return this.configurations ?? (this.configurations = this.GetConfigurations());
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                // only read from cache if enabled and no switchers are active
                if (this.ConfigurationManager.CachingEnabled && !this.ConfigurationManager.HasActiveSwitchers)
                {
                    var cachedResult = false;
                    if (this.GetCachedValidationResult(ref cachedResult)) return cachedResult;
                }

                // run the validation
                Log.Debug("Validating ruleset: " + this, this);
                var result = this.ruleValidator.Validate(this.Rules);
                Log.Debug("Ruleset " + this + " is valid: " + result, this);

                // only write to cache if enabled and no switchers are active
                if (this.ConfigurationManager.CachingEnabled && !this.ConfigurationManager.HasActiveSwitchers)
                {
                    this.AddValidationResultToCache(result);
                }

                return result;
            }
        }

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
        public bool CanSkipValidation<TType>()
        {
            var canSkip = !this.Configurations.ContainsKey(typeof(TType));
            if (canSkip)
            {
                var message = string.Format("Skipping ruleset possible ({0}). No such configuration defined: {1}", this, typeof(TType).Name);
                Log.Debug(message, this);
            }

            return canSkip;
        }

        /// <summary>
        /// Returns the name of the ruleset.
        /// </summary>
        /// <returns>
        /// The name of the ruleset.
        /// </returns>
        public override string ToString()
        {
            return this.item.Name;
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <returns>
        /// The cache key.
        /// </returns>
        protected virtual string GetCacheKey()
        {
            return "rs_" + this.item.ID;
        }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <returns>
        /// The rules.
        /// </returns>
        protected virtual ConfigurationRuleList GetRules()
        {
            var ruleList = RuleFactory.GetRules<ConfigurationRuleContext>(new[] { this.item }, "Rules");
            return new ConfigurationRuleList(ruleList);
        }

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <returns>
        /// The configurations.
        /// </returns>
        protected virtual IDictionary<Type, IConfiguration> GetConfigurations()
        {
            var configs = new Dictionary<Type, IConfiguration>();
            var templateId = this.settings.ConfigurationTemplateId;
            foreach (var configurationItem in ((MultilistField)this.item.Fields["Configurations"]).GetItems().Where(configItem => configItem.HasBaseTemplate(templateId)))
            {
                var fullType = configurationItem["Type"];
                var configuration = ConfigurationFactory.Create(fullType, configurationItem);
                if (configuration == null) continue;

                var type = configuration.GetType();
                if (!configs.ContainsKey(type))
                {
                    configs.Add(type, configuration);
                }
            }

            return configs;
        }

        /// <summary>
        /// Gets the cached validation result.
        /// </summary>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>The cached validation result.</returns>
        private bool GetCachedValidationResult(ref bool result)
        {
            var context = HttpContext.Current;
            if (context == null) return false;

            var cacheKey = this.GetCacheKey();
            var cachedValue = context.Items[cacheKey];
            if (cachedValue == null) return false;

            result = (bool)cachedValue;

            Log.Debug("Ruleset " + this + " is valid (cached): " + result, this);
            return true;
        }

        /// <summary>
        /// Adds the validation result to cache.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void AddValidationResultToCache(bool value)
        {
            var context = HttpContext.Current;
            if (context == null) return;

            Log.Debug("Adding validation result for " + this + " to request cache", this);

            var cacheKey = this.GetCacheKey();
            context.Items[cacheKey] = value;
        }
    }
}
