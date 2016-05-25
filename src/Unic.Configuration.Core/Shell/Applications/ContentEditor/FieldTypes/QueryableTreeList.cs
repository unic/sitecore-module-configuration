namespace Unic.Configuration.Core.Shell.Applications.ContentEditor.FieldTypes
{
    using Sitecore;
    using Sitecore.Shell.Applications.ContentEditor;

    /// <summary>
    /// The queryable treelist.
    /// More information: http://www.cognifide.com/blogs/sitecore/reduce-multisite-chaos-with-sitecore-queries/
    /// -------------------------------------------------------------------------------------------------------
    /// To register your new field type:
    /// - switch to core database
    /// - duplicate /sitecore/system/Field types/List Types/Treelist item
    /// - rename it to Queryable Treelist
    /// - clear Control field, and fill in Assembly and Class fields.
    /// -------------------------------------------------------------------------------------------------------
    /// Now you can use queries in the Source field of the customized Treelist, i.e.
    /// query:./ancestor-or-self::*[@@templatename='HomeTemplate']/Settings/SocialMedia.
    /// </summary>
    public class QueryableTreeList : TreeList
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public new string Source
        {
            get
            {
                return base.Source;
            }

            set
            {
                var dataSource = StringUtil.ExtractParameter("DataSource", value).Trim();
                if (dataSource.StartsWith("query:"))
                {
                    base.Source = value.Replace(dataSource, this.ResolveQuery(dataSource));
                }
                else
                {
                    base.Source = value.StartsWith("query:") ? this.ResolveQuery(value) : value;
                }
            }
        }

        /// <summary>
        /// Resolves the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The path to the queried item</returns>
        private string ResolveQuery(string query)
        {
            query = query.Substring("query:".Length);
            var contextItem = Sitecore.Context.ContentDatabase.Items[this.ItemID];
            var queryItem = contextItem.Axes.SelectSingleItem(query);

            return queryItem != null ? queryItem.Paths.FullPath : string.Empty;
        }
    }
}