using System;

namespace AirSnitch.Api.Resources
{
    internal class ApiResourceName : IEquatable<ApiResourceName>
    {
        private readonly string _name;

        public ApiResourceName(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public bool Equals(ApiResourceName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ApiResourceName) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}