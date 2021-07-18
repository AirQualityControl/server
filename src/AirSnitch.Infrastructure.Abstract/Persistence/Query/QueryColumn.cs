using System;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Query
{
    public class QueryColumn : IEquatable<QueryColumn>
    {
        public QueryColumn(string name, string path)
        {
            Name = name;
            Path = path;
        }
        public string Name { get; }

        public string Path { get; }

        public bool Equals(QueryColumn other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Path == other.Path;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((QueryColumn) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Path);
        }
    }
    
    public class PrimaryColumn : QueryColumn
    {
        private static readonly string _primaryColumnName = "id";
        private static readonly string _primaryColumnValue = "_id";
        
        public PrimaryColumn() : base(_primaryColumnName, _primaryColumnValue)
        {
            
        }
    }
}