namespace AirSnitch.Api.Rest.Resources
{
    public class ApiResourceColumn
    {
        private readonly string _name;
        private readonly string _path;

        public ApiResourceColumn(string name, string path)
        {
            _name = name;
            _path = path;
        }

        public string Name => _name;

        public string Path => _path;
    }
}