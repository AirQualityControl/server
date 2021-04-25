using System;

namespace AirSnitch.Core.Domain.Models
{
    
    public class AirPollutionDataProviderTag 
    {
        public AirPollutionDataProviderTag(string stringValue)
        {
            //TODO:validation
            Value = stringValue;
        }

        public string Value { get; }

        public bool Equals(AirPollutionDataProviderTag other)
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
            return Equals((AirPollutionDataProviderTag) obj);
        }
        
        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
}