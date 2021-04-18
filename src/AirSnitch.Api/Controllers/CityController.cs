using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : ControllerBase
    {

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return await Task.FromResult(Ok($"city stub for id: {id}"));
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {
            return await Task.FromResult(Ok($"city collection stub limit: {limit}, offset: {offset}"));
        }
    }
}