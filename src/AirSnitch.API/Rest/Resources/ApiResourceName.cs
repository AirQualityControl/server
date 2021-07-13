using System;

namespace AirSnitch.Api.Rest.Resources
{
    public class ApiResourceName : IEquatable<ApiResourceName>
    {
        private readonly string _value;

        public ApiResourceName(string value)
        {
            _value = value;
        }

        public string Value => _value;

        public bool Equals(ApiResourceName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
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
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}