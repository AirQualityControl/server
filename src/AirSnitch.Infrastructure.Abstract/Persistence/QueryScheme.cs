using System;
using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class QueryScheme
    {
        private readonly List<QueryColumn> _columns = new List<QueryColumn>();
        private readonly List<IColumnFilter> _filters = new List<IColumnFilter>();

        private PageOptions _pageOptions;
        
        public string EntityName { get; set; }

        public IReadOnlyCollection<QueryColumn> Columns => _columns;

        public PageOptions PageOptions => _pageOptions;

        public IReadOnlyCollection<IColumnFilter> Filters => _filters;
        
        public void AddColumn(QueryColumn column)
        {
            _columns.Add(column);
        }

        public void AddPageOptions(PageOptions pageOptions)
        {
            _pageOptions = pageOptions;
        }

        public void AddColumnFilter(IColumnFilter columnFilter)
        {
            //TODO: check whether scheme contains such filter column
            _filters.Add(columnFilter);
        }
    }
}