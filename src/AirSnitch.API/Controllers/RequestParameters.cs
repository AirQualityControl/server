using System.ComponentModel.DataAnnotations;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    public class RequestParameters
    {
        [FromQuery(Name = "page")]
        [Range(1, int.MaxValue, ErrorMessage = "Value of 'page' parameter should be between 1 and 100")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        [Range(1, 100, ErrorMessage = "Value of 'pageSize' parameter should be between 1 and 100")]
        public int PageSize { get; set; } = 50;

        [FromQuery(Name = "includes")]
        public string IncludesString { get; set; }
        internal PageOptions PageOptions => new PageOptions(PageNumber, PageSize);
    }
}