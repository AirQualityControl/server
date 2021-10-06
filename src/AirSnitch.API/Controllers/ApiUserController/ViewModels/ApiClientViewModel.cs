using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AirSnitch.Domain.Models;

namespace AirSnitch.Api.Controllers.ApiUser.ViewModels
{
    internal class ApiClientViewModel
    {
        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public static List<ApiClientViewModel> BuildFrom(IReadOnlyCollection<ApiClient> clients)
        {
            return clients.Select(
                c => new ApiClientViewModel()
                {
                    Id = c.Id,
                    CreatedOn = c.CreatedOn.ToString(CultureInfo.InvariantCulture),
                    Name = c.Name.Value,
                    Description = c.Description.Value,
                    Type = c.Type.ToString()
                }
            ).ToList();
        }
    }
}