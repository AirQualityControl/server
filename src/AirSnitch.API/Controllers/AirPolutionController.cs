using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.API.Controllers
{
    [ApiController]
    [Route("airpolution")]
    public class AirPolutionController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get(double lat, double lng, double radius)
        {
            return await Task.FromResult(Ok($"Air polution stub for lat: {lat}," +
                $" lng: {lng}, radius: {radius}"));
        }
    }
}
