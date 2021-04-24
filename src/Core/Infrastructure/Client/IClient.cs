using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Core.Infrastructure.Client
{
    /// <summary>
    /// Interface that represent user Client (Telegram bot, facebook bot etc)
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Return basic information about current client.
        /// </summary>
        /// <returns></returns>
        ClientInfo GetInfo();
    }
}
