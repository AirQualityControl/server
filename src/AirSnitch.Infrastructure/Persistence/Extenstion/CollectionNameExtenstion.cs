using System;
using System.Linq;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

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
            Require.That(originalString, Is.NotNull);
            Require.That(originalString, Is.NotNullOrEmptyString); 
           
            char firstLetter = originalString.First();
            return originalString.Replace(firstLetter, Char.ToLower(firstLetter));
        }
    }
}