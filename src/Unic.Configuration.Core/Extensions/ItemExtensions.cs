namespace Unic.Configuration.Core.Extensions
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;

    /// <summary>
    /// The item extensions.
    /// </summary>
    public static class ItemExtensions
    {
        /// <summary>
        /// Validates the specified item against a base template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="baseTemplate">The base template name or ID.</param>
        /// <returns>If the item has the base template.</returns>
        public static bool HasBaseTemplate(this Item item, string baseTemplate)
        {
            if (item == null || string.IsNullOrWhiteSpace(baseTemplate)) return false;

            var template = TemplateManager.GetTemplate(item);
            if (template == null) return false;

            if (ID.IsID(baseTemplate) || ShortID.IsShortID(baseTemplate))
            {
                return template.InheritsFrom(new ID(baseTemplate));
            }

            return template.InheritsFrom(baseTemplate);
        }
    }
}
