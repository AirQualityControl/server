using AirSnitch.Api.Infrastructure.Authorization;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.Services;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [Authorize(Policy = Policies.RequiredUser)]
    [ApiController]
    [Route(ControllersRoutes.City)]
    public class CityController : BaseApiController
    {
        private ICityService _cityService;
        public CityController(ICityService cityService,
            IResoursePathResolver resoursePathResolver) : base(resoursePathResolver)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<CityDTO>>> Get(string id)
        {
            var basePath = ControllerContext.HttpContext.Request.Path.Value;
            var model = await _cityService.GetByIdAsync(id);
            return Ok(await CreateResponseObjectAsync(basePath, model));
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {
            limit = limit > 0 ? limit : 10;
            (var paginatedResult, var total) = await _cityService.GetPaginated(limit, offset);
            return Ok(await CreatePaginativeResponseObjectAsync(limit, offset, total, paginatedResult));
        }

        protected override Task<object> GetIncludeObject(string include, string id)
        {
            throw new ArgumentException($"Incorrect include: {include}");
        }
    }
}
