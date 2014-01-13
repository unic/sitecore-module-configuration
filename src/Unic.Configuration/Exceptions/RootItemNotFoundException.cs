namespace Unic.Configuration.Exceptions
{
    using System;

    /// <summary>
    /// The root item not found exception.
    /// </summary>
    [Serializable]
    public class RootItemNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootItemNotFoundException" /> class.
        /// </summary>
        public RootItemNotFoundException() : base(BuildMessage())
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
            return "Root item could not be found.";
        }
    }
}
