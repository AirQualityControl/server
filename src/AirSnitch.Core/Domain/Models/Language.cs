using System;
using AirSnitch.Core.Domain.Exceptions;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// ADT that represent a language that user will user 
    /// </summary>
    public class Language : IDomainModel<Language>
    {
        /// <summary>
        /// String representation of language code value
        /// </summary>

        private string _code;
        public string Code {
            get => _code;
            set
            {
                _code = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Language) obj);
        }

        public override int GetHashCode()
        {
            return _code.GetHashCode();
        }

        public bool Equals(Language other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _code == other._code;
        }

        public object Clone()
        {
            return new Language()
            {
                Code = this._code
            };
        }

        public bool IsEmpty { get; set; }
        public bool IsValid()
        {
            if (IsEmpty)
            {
                return true;
            }
            return !String.IsNullOrEmpty(_code);
        }

        public void Validate()
        {
            if (!IsEmpty)
            {
                if (String.IsNullOrEmpty(_code))
                {
                    throw new InvalidEntityStateException("Entity state is not valid.Code should be not null or empty string.");
                }
            }
        }
    }
}