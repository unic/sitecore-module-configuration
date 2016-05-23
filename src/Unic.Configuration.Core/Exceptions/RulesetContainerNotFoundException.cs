namespace Unic.Configuration.Core.Exceptions
{
    using System;

    /// <summary>
    /// The ruleset container not found exception.
    /// </summary>
    [Serializable]
    public class RulesetContainerNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RulesetContainerNotFoundException"/> class.
        /// </summary>
        public RulesetContainerNotFoundException() : base(BuildMessage())
        {
        }

        /// <summary>
        /// Builds the message.
        /// </summary>
        /// <returns>
        /// Error message
        /// </returns>
        private static string BuildMessage()
        {
            return "Ruleset container could not be found.";
        }
    }
}
