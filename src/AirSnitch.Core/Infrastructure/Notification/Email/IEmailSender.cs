using System.Threading.Tasks;

namespace AirSnitch.Core.Infrastructure.Notification.Email
{
    public interface IEmailSender
    {
        /// <summary>
        /// Send email from the system
        /// </summary>
        /// <param name="emailToSend">Email that will be sent</param>
        /// <returns>Task of asynchronous operation</returns>
        Task SendAsync(Email emailToSend);
    }
}