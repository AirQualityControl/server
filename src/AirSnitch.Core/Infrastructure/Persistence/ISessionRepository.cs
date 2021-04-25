using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Core.Infrastructure.Persistence
{
    /// <inheritdoc />
    public interface ISessionRepository
    {
        Task Insert(Session session);
    }
}
