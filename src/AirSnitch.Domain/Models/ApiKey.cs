using System;
using System.Security.Cryptography;

namespace AirSnitch.Domain.Models
{
    public class ApiKey
    {
        private static ApiKey _empty = new ApiKey(String.Empty);
        
        private ApiKey(string value)
        {
            Value = value;
        }

        /// <summary>
        /// String representation of api key
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Get the date and time when api key was issued.
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Get the date when api key become invalid
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Empty instance of api key
        /// </summary>
        public static ApiKey Empty => _empty;

        /// <summary>
        /// Generate a new api key for user
        /// </summary>
        /// <returns></returns>
        public static ApiKey Generate()
        {
            var bytesHolder = new byte[32];
            
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(bytesHolder);    
            }
            string value = Convert.ToBase64String(bytesHolder);

            return new ApiKey(value)
            {
                IssueDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMonths(4),
            };
        }

        /// <summary>
        /// Method that checks weather current api key is expired or not
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            return false;
        }

        public static ApiKey FromString(string apiKeyString)
        {
            return new ApiKey(apiKeyString);
        }

        /// <summary>
        /// Checks weather api key is valid or not
        /// </summary>
        /// <returns></returns>
        public static bool IsValid(string apiKeyString)
        {
            return true;
        }
    }
}