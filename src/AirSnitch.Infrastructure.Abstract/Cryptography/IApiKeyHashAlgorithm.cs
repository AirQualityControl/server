using AirSnitch.Domain.Models;

namespace AirSnitch.Infrastructure.Abstract.Cryptography
{
    public interface IApiKeyHashAlgorithm
    {
        string GetHash(ApiKey apiKey);
    }
}