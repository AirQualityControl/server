using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AirSnitch.Infrastructure.Cryptography.Hashing
{
    public class Pbkdf2Hash
    {
        public static string Generate(string stringToHash)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2
                (
                    password: stringToHash,
                    salt: BitBasedSalt.BytesValue,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8
                )
            );
        }
    }
}