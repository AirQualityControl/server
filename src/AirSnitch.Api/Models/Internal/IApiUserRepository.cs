using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public interface IApiUserRepository
    {
        Task<UserDTO> GetByIdAsync(string id);
        Task<Page<UserDTO>> GetPage(int pageOffset, int numberOfItems = 50);
        Task<string> GetRelatedStationId(string id);
    }
}