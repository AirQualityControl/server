using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AirSnitch.Infrastructure.Cryptography.Hashing
{
    public class Pbkdf2Hash
    {
        private static byte[] _salt = Convert.FromBase64String("sePmpdNzIG0ASbIx6u7BMkqY2Ls+XXbEQOM18v24qOM=");
        
        public static string Generate(string stringToHash)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2
                (
                    password: stringToHash,
                    salt: _salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 50000,
                    numBytesRequested: 256 / 8
                )
            );
        }
    }
}