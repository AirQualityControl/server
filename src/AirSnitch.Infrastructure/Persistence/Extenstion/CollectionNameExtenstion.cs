using System;
using System.Diagnostics.Contracts;
using System.Linq;



namespace AirSnitch.Infrastructure.Persistence.Extenstion
{
    internal static class CollectionNameExtenstion
    {
        /// <summary>
        /// Returns new string on lowerCamelCase formatting
        /// </summary>
        /// <param name="originalString">Incoming string
        ///Preconditions:
        ///     String is not null;
        ///     String is not Empty;
        /// </param>
        /// <returns>New string in lowerCamelCase formatting</returns>
        public static string ToLowerCamelCase(this String originalString)
        {
            Contract.Requires(originalString != null);
            Contract.Requires(!String.IsNullOrEmpty(originalString));
           
            char firstLetter = originalString.First();
            return originalString.Replace(firstLetter, Char.ToLower(firstLetter));
        }
    }
}