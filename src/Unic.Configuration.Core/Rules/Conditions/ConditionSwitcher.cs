namespace Unic.Configuration.Core.Rules.Conditions
{
    using System.Threading;
    using Sitecore.Common;

    /// <summary>
    /// The condition switcher base class.
    /// </summary>
    /// <typeparam name="T">The value to switch to.</typeparam>
    public abstract class ConditionSwitcher<T> : Switcher<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionSwitcher{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected ConditionSwitcher(T value) : base(value)
        {
            Interlocked.Increment(ref ConditionSwitcherCounter.ActiveSwitchers);
            this.Disposed += (sender, args) => Interlocked.Decrement(ref ConditionSwitcherCounter.ActiveSwitchers);
        }

        /// <summary>
        /// Gets the active switchers count.
        /// </summary>
        /// <value>
        /// The active switchers count.
        /// </value>
        public static int ActiveSwitchersCount
        {
            get
            {
                return Interlocked.Add(ref ConditionSwitcherCounter.ActiveSwitchers, 0);
            }
        }
    }
}
