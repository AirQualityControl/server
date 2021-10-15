using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Cryptography;

namespace AirSnitch.Infrastructure.Cryptography.Hashing
{
    public class Pbkdf2HashAlgorithm : IApiKeyHashAlgorithm
    {
        public string GetHash(ApiKey apiKey)
        {
            return Pbkdf2Hash.Generate(apiKey.Value);
        }
    }
}