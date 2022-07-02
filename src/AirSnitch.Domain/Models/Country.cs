using System;

namespace AirSnitch.Domain.Models
{
    public class Country
    {
        internal Country(string code)
        {
            Code = code;
        }

        public string Code { get; }

        public static Country UA => new Country("UA");
    }
}