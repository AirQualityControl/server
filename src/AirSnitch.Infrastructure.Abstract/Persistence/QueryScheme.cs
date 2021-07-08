using System;
using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class QueryScheme
    {
        private readonly List<QueryColumn> _columns = new List<QueryColumn>();

        private PageOptions _pageOptions;
        
        public string EntityName { get; set; }

        public IReadOnlyCollection<QueryColumn> Columns => _columns;

        public PageOptions PageOptions => _pageOptions;
        
        public void AddColumn(QueryColumn column)
        {
            _columns.Add(column);
        }

        public void AddPageOptions(PageOptions pageOptions)
        {
            _pageOptions = pageOptions;
        }
    }
}