using System;
using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class FetchQuery
    {
        private readonly Lazy<List<QueryColumn>> _columns = new Lazy<List<QueryColumn>>();
        public string EntityName { get; set; }
        public void AddColumn(QueryColumn column)
        {
            _columns.Value.Add(column);
        }
    }
}