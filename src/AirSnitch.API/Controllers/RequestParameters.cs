using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    public class RequestParameters
    {
        [FromQuery(Name = "page")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "includes")]
        public string Includes { get; set; }
        internal PageOptions PageOptions => new PageOptions(PageNumber, PageSize);
    }
}