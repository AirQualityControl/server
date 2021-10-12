using System;
using System.Security.Cryptography;

namespace AirSnitch.Infrastructure.Cryptography.Hashing
{
    public class BitBasedSalt
    {
        public static byte[] BytesValue
        {
            get
            {
                byte[] salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                return salt;
            }
        }
    }
}