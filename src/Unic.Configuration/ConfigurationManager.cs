namespace Unic.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.SecurityModel;
    using Unic.Configuration.Exceptions;
    using Unic.Configuration.Extensions;
    using Unic.Configuration.Rules.Conditions;

    /// <summary>
    /// The configuration manager loads the correct configuration value from 
    /// a configuration object.
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// The lock object.
        /// </summary>
        private readonly object lockObject = new object();

        /// <summary>
        /// The settings.
        /// </summary>
        private readonly Settings settings;

        /// <summary>
        /// The root ruleset containers.
        /// </summary>
        private IDictionary<string, IRulesetContainer> rootRulesetContainers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager" /> class.
        /// </summary>
        public ConfigurationManager() : this(TypeResolver.Settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ConfigurationManager(Settings settings)
        {
            this.settings = settings;
            this.CachingEnabled = true;
        }

        /// <summary>
        /// Gets the site root item.
        /// </summary>
        /// <value>
        /// The site root item.
        /// </value>
        public Item SiteRoot
        {
            get
            {
                return this.GetRootItem();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether caching is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if caching is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool CachingEnabled { get; set; }

        /// <summary>
        /// Gets a value indicating whether it has active switchers.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has active switchers; otherwise, <c>false</c>.
        /// </value>
        public bool HasActiveSwitchers
        {
            get
            {
                return ConditionSwitcherCounter.ActiveSwitchers > 0;
            }
        }

        /// <summary>
        /// Gets the root ruleset containers.
        /// </summary>
        /// <value>
        /// The root ruleset containers.
        /// </value>
        protected IDictionary<string, IRulesetContainer> RootRulesetContainers
        {
            get
            {
                return this.rootRulesetContainers ?? (this.rootRulesetContainers = new Dictionary<string, IRulesetContainer>());
            }
        }
        
        /// <summary>
        /// Invalidates all configurations.
        /// </summary>
        public void InvalidateConfigurations()
        {
            lock (this.lockObject)
            {
                this.RootRulesetContainers.Clear();
            }

            Log.Info("Configuration cache cleared", this);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public string Get<TType>(Expression<Func<TType, string>> func) where TType : class
        {
            return this.Get<TType, string>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public bool Get<TType>(Expression<Func<TType, bool>> func) where TType : class
        {
            return this.Get<TType, bool>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public int Get<TType>(Expression<Func<TType, int>> func) where TType : class
        {
            return this.Get<TType, int>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public double Get<TType>(Expression<Func<TType, double>> func) where TType : class
        {
            return this.Get<TType, double>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public DateTime Get<TType>(Expression<Func<TType, DateTime>> func) where TType : class
        {
            return this.Get<TType, DateTime>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public Item Get<TType>(Expression<Func<TType, Item>> func) where TType : class
        {
            return this.Get<TType, Item>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public IEnumerable<Item> Get<TType>(Expression<Func<TType, IEnumerable<Item>>> func) where TType : class
        {
            return this.Get<TType, IEnumerable<Item>>(func) ?? Enumerable.Empty<Item>();
        }

        /// <summary>
        /// Gets the specified configuration field.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration field.
        /// </returns>
        public IConfigurationField Get<TType>(Expression<Func<TType, IConfigurationField>> func) where TType : class
        {
            return this.Get<TType, IConfigurationField>(func);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the configuration value.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        public TValue Get<TType, TValue>(Expression<Func<TType, TValue>> func) where TType : class
        {
            // get the root ruleset container
            var container = this.GetRootRulesetContainer();

            // get the configuration value
            return this.GetConfigurationValue(func, container);
        }

        /// <summary>
        /// Gets the ruleset container.
        /// </summary>
        /// <returns>
        /// The ruleset container.
        /// </returns>
        protected virtual IRulesetContainer GetRootRulesetContainer()
        {
            var cacheKey = this.GetContainerCacheKey();
            if (!this.RootRulesetContainers.ContainsKey(cacheKey))
            {
                lock (this.lockObject)
                {
                    if (!this.RootRulesetContainers.ContainsKey(cacheKey))
                    {
                        var containerItem = this.GetRootRulesetContainerItem();

                        Log.Debug("Add new ruleset container for key: " + cacheKey, this);
                        this.RootRulesetContainers.Add(cacheKey, new RulesetContainer(cacheKey, containerItem, this));
                    }
                }
            }

            return this.RootRulesetContainers[cacheKey];
        }

        /// <summary>
        /// Gets the ruleset container item.
        /// </summary>
        /// <returns>
        /// The ruleset container item.
        /// </returns>
        /// <exception cref="Unic.Configuration.Exceptions.RootItemNotFoundException">The root item could not be found.</exception>
        /// <exception cref="Unic.Configuration.Exceptions.RulesetContainerNotFoundException">The ruleset container item could not be found.</exception>
        protected virtual Item GetRootRulesetContainerItem()
        {
            // return custom root ruleset container item, if one is configured
            var customRootRulesetContainerItemId = this.settings.CustomRootRulesetContainerItemId;
            if (!string.IsNullOrWhiteSpace(customRootRulesetContainerItemId) && ID.IsID(customRootRulesetContainerItemId))
            {
                return this.GetDatabase().GetItem(customRootRulesetContainerItemId);
            }

            // find the root ruleset container item in the children of the root item
            var rootItem = this.GetRootItem();
            if (rootItem == null) throw new RootItemNotFoundException();

            var templateId = this.settings.RulesetContainerTemplateId;
            foreach (var rulesetContainerItem in rootItem.Children.Where(child => child.HasBaseTemplate(templateId)))
            {
                return rulesetContainerItem;
            }

            throw new RulesetContainerNotFoundException();
        }

        /// <summary>
        /// Gets the root item of the current item. It starts the search from the start path up the tree.
        /// </summary>
        /// <returns>
        /// The root item of the current item.
        /// </returns>
        protected virtual Item GetRootItem()
        {
            using (new SecurityDisabler())
            {
                var rootItemTemplateIds = this.settings.RootItemTemplateIds.ToArray();

                var startPath = this.GetStartPath();
                var workItem = this.GetDatabase().GetItem(startPath);

                while (!rootItemTemplateIds.Contains(workItem.TemplateID.ToString()))
                {
                    if (workItem.Parent == null) break;
                    workItem = workItem.Parent;
                }

                return rootItemTemplateIds.Contains(workItem.TemplateID.ToString()) ? workItem : null;
            }
        }

        /// <summary>
        /// Gets the start path.
        /// </summary>
        /// <returns>The start path.</returns>
        protected virtual string GetStartPath()
        {
            return Sitecore.Context.Site.StartPath;
        }

        /// <summary>
        /// Gets the current database from the Sitecore context.
        /// </summary>
        /// <returns>The context database</returns>
        protected virtual Database GetDatabase()
        {
            return Sitecore.Context.Database;
        }

        /// <summary>
        /// Gets the container cache key.
        /// </summary>
        /// <returns>The container cache key.</returns>
        protected virtual string GetContainerCacheKey()
        {
            return string.Join("_", Sitecore.Context.Site.Name, Sitecore.Context.Language);
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the configuration value.</typeparam>
        /// <param name="func">The property.</param>
        /// <returns>The cache key.</returns>
        protected virtual string GetConfigurationCacheKey<TType, TValue>(Expression<Func<TType, TValue>> func)
        {
            return GetConfigurationPropertyName(func);
        }

        /// <summary>
        /// Gets the name of the configuration property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>The name of the configuration property.</returns>
        private static string GetConfigurationPropertyName<TType, TValue>(Expression<Func<TType, TValue>> func)
        {
            var memberExpression = (MemberExpression)func.Body;
            return string.Format("{0}.{1}", memberExpression.Expression.Type.Name, memberExpression.Member.Name);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the configuration value.</typeparam>
        /// <param name="func">The property.</param>
        /// <param name="container">The ruleset container.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        private TValue GetConfigurationValue<TType, TValue>(Expression<Func<TType, TValue>> func, IRulesetContainer container) where TType : class
        {
            var configurationName = GetConfigurationPropertyName(func);
            Log.Debug("Getting configuration for " + configurationName + " from " + container, this);

            // only read from cache if enabled and no switchers are active
            if (this.CachingEnabled && !this.HasActiveSwitchers)
            {
                var cachedValue = default(TValue);
                if (this.GetCachedConfigurationValue(func, ref cachedValue)) return cachedValue;
            }

            var configurationBase = this.GetConfiguration(func, container.Rulesets);
            var configuration = configurationBase as TType;
            if (configuration == null)
            {
                if (container.FallbackContainer == null)
                {
                    var defaultValue = default(TValue);
                    Log.Debug("Could not find configuration for " + configurationName + ". Returning default value: " + defaultValue, this);
                    return defaultValue;
                }

                Log.Debug("Falling back for " + configurationName + " on " + container, this);
                return this.GetConfigurationValue(func, container.FallbackContainer);
            }

            var value = func.Compile().Invoke(configuration);
            Log.Debug("Found configuration for " + configurationName + " from " + container + " (" + value + ")", this);

            // only write to cache if enabled and no switchers are active
            if (this.CachingEnabled && !this.HasActiveSwitchers)
            {
                this.AddConfigurationValueToCache(func, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the cached configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="func">The function.</param>
        /// <param name="result">The result.</param>
        /// <returns>
        /// Return true if a caching value could be found.
        /// </returns>
        private bool GetCachedConfigurationValue<TType, TValue>(Expression<Func<TType, TValue>> func, ref TValue result) where TType : class
        {
            var context = HttpContext.Current;
            if (context == null) return false;

            var cacheKey = this.GetConfigurationCacheKey(func);
            var cachedValue = context.Items[cacheKey];
            if (cachedValue == null) return false;

            result = (TValue)cachedValue;

            var configurationName = GetConfigurationPropertyName(func);
            Log.Debug("Found configuration for " + configurationName + " in the request cache (" + result + ")", this);

            return true;
        }

        /// <summary>
        /// Adds the configuration value to cache.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="func">The function.</param>
        /// <param name="value">The value.</param>
        private void AddConfigurationValueToCache<TType, TValue>(Expression<Func<TType, TValue>> func, TValue value) where TType : class
        {
            var context = HttpContext.Current;
            if (context == null) return;

            var configurationName = GetConfigurationPropertyName(func);
            Log.Debug("Adding configuration for " + configurationName + " to request cache", this);

            var cacheKey = this.GetConfigurationCacheKey(func);
            context.Items[cacheKey] = value;
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the configuration value.</typeparam>
        /// <param name="func">The property.</param>
        /// <param name="rulesets">The rulesets.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        private IConfiguration GetConfiguration<TType, TValue>(Expression<Func<TType, TValue>> func, IEnumerable<IRuleset> rulesets)
        {
            return rulesets.Select(ruleset => this.GetConfiguration(func, ruleset)).FirstOrDefault(configuration => configuration != null);
        }

        /// <summary>
        /// Gets the specified configuration value.
        /// </summary>
        /// <typeparam name="TType">The type of the configuration.</typeparam>
        /// <typeparam name="TValue">The type of the configuration value.</typeparam>
        /// <param name="func">The property.</param>
        /// <param name="ruleset">The ruleset.</param>
        /// <returns>
        /// The configuration value.
        /// </returns>
        private IConfiguration GetConfiguration<TType, TValue>(Expression<Func<TType, TValue>> func, IRuleset ruleset)
        {
            if (ruleset.CanSkipValidation<TType>() || !ruleset.IsValid) return null;

            var configuration = this.GetConfiguration<TType>(ruleset.Configurations);
            if (configuration != null && configuration.HasValueFor(func))
            {
                return configuration;
            }

            // TODO (TSt): Remove fallback ruleset implementation
            return ruleset.FallbackRuleset != null ? this.GetConfiguration(func, ruleset.FallbackRuleset) : null;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <typeparam name="TType">The configuration type.</typeparam>
        /// <param name="configs">The configurations.</param>
        /// <returns>The configuration.</returns>
        private IConfiguration GetConfiguration<TType>(IDictionary<Type, IConfiguration> configs)
        {
            return configs.ContainsKey(typeof(TType)) ? configs[typeof(TType)] : null;
        }
    }
}
