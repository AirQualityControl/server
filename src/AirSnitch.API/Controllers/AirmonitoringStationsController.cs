using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("airmonitoringstations")]
    public class AirmonitoringStationsController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {
            return await Task.FromResult(Ok($"ListOfStations limit: {limit}, offset: {offset}"));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetWithIncludes(int id, [FromQuery] string include)
        {
            if (!String.IsNullOrEmpty(include))
            {
                return await Task.FromResult(Ok($"stationId = {id} include = " +
                    include.Trim().Split(',').Aggregate((x, y) => $"{x};{y}")));
            }
            return await Task.FromResult(Ok($"stationId = {id}"));
        }

        [HttpGet]
        [Route("{id}/airPollution")]
        public async Task<ActionResult> GetAirPolution(int id)
        {
            return await Task.FromResult(Ok($"airpolution info from stationId = {id}"));
        }

        [HttpGet]
        [Route("{id}/dataprovider")]
        public async Task<ActionResult> GetDataProvider(int id)
        {
            return await Task.FromResult(Ok($"dataprovier info from stationId = {id}"));
        }

        [HttpGet]
        [Route("{id}/airPollutionHistory")]
        public async Task<ActionResult> GetAirPolutionHistory(int id)
        {
            return await Task.FromResult(Ok($"Air polution history info from stationId = {id}"));
        }

        [HttpGet]
        [Route("{id}/city")]
        public async Task<ActionResult> GetCity(int id)
        {
            return await Task.FromResult(Ok($"City info from stationId = {id}"));
        }

    }
}
