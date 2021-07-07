namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class QueryColumn
    {
        public QueryColumn(string name, string path)
        {
            Name = name;
            Path = path;
        }
        public string Name { get; }

        public string Path { get; }
    }
}