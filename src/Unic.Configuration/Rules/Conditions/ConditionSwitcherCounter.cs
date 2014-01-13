namespace Unic.Configuration.Rules.Conditions
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The condition switcher counter.
    /// </summary>
    public static class ConditionSwitcherCounter
    {
        /// <summary>
        /// The active switchers.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "A public field is necessary here.")]
        internal static int ActiveSwitchers;
    }
}
