using System.Collections.Generic;
using System.Linq;
using AirSnitch.Domain.Models;

namespace AirSnitch.Api.Controllers
{
    internal class ApiClientViewModel
    {
        public string Name;

        public string Description;

        public string Type;

        public static List<ApiClientViewModel> BuildFrom(IReadOnlyCollection<ApiClient> clients)
        {
            return clients.Select(
                c => new ApiClientViewModel()
                {
                    Name = c.Name.Value,
                    Description = c.Description.Value,
                    Type = c.Type.ToString()
                }
            ).ToList();
        }
    }
}