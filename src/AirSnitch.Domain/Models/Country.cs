using System;

namespace AirSnitch.Domain.Models
{
    public class Country
    {
        public static Country UA => new Country();

        private static void FromString(string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}