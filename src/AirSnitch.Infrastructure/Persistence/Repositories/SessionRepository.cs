using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Persistence;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <inheritdoc cref="ISessionRepository" />
    internal class SessionRepository : BaseRepository<Session>,  ISessionRepository
    {
        public async Task Insert(Session session)
        {
            await base.SaveAsync(session);
        }
    }
}
