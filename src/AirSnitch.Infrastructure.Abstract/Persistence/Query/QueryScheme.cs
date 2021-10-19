using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract.Persistence.Exceptions;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Query
{
    /// <summary>
    /// ADT - that represent an abstract query scheme.
    /// valid query scheme might be interpret to any valid query.
    /// </summary>
    public class QueryScheme
    {
        private readonly List<QueryColumn> _columns = new List<QueryColumn>();
        private readonly List<IColumnFilter> _filters = new List<IColumnFilter>();
        private PageOptions _pageOptions;
        
        /// <summary>
        /// Entity name 
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Returns query scheme read-only collection of Query columns
        /// </summary>
        public IReadOnlyCollection<QueryColumn> Columns => _columns;

        /// <summary>
        /// Returns query scheme page options
        /// </summary>
        public PageOptions PageOptions => _pageOptions;
        
        /// <summary>
        ///     Returns read-only collection of added columns filters.
        /// </summary>
        public IReadOnlyCollection<IColumnFilter> Filters => _filters;

        /// <summary>
        /// Adds a new QueryColumn to current query scheme
        /// </summary>
        /// <param name="column">Query column</param>
        public void AddColumn(QueryColumn column)
        {
            _columns.Add(column);
        }

        /// <summary>
        /// Adds a new QueryColumn to current query scheme
        /// </summary>
        /// <param name="pageOptions">Page options</param>
        public void AddPageOptions(PageOptions pageOptions)
        {
            _pageOptions = pageOptions;
        }

        /// <summary>
        /// Adds a new column filter to current query scheme
        /// </summary>
        /// <param name="columnFilter">Column filter</param>
        public void AddColumnFilter(IColumnFilter columnFilter)
        {
            _filters.Add(columnFilter);
        }
    }
}