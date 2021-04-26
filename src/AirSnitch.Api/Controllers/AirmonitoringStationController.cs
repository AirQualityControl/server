using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.Authorization;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using AirSnitch.Api.Infrastructure.Services;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [Authorize(Policy = Policies.RequiredUser)]
    [ApiController]
    [IncludeResourse(ControllersRoutes.City)]
    [IncludeResourse(ControllersRoutes.Dataprovider)]
    [IncludeResourse(ControllersRoutes.AirPolution)]
    [Route(ControllersRoutes.AirmonitoringStation)]
    public class AirMonitoringStationController : BaseApiController
    {
        private IAirMonitoringStationService _airMonitoringStationService;
        public AirMonitoringStationController(IResoursePathResolver resoursePathResolver,
            IAirMonitoringStationService airMonitoringStationService) : base(resoursePathResolver)
        {
            _airMonitoringStationService = airMonitoringStationService;
        }
        

        protected override async Task<object> GetIncludeObject(string include, string id)
        {
            return include switch
            {
                "airpolution" => await  _airMonitoringStationService.GetIncludedAirpolution(id),
                "city" => await _airMonitoringStationService.GetIncludedCity(id),
                "dataproviders" => await _airMonitoringStationService.GetIncludedDataProvider(id),
                _ => throw new ArgumentException($"Incorrect include: {include}"),
            };
        }

        [HttpGet]
        public async Task<ActionResult<PaginativeResponse<AirMonitoringStationDTO>>> GetPaginated(int limit, int offset)
        {
            limit = limit > 0 ? limit : 10;
            (var paginatedResult, var total) = await _airMonitoringStationService.GetPaginated(limit, offset);
            return Ok(await CreatePaginativeResponseObjectAsync(limit, offset, total, paginatedResult));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<AirMonitoringStationDTO>>> GetWithIncludes(string id, [FromQuery] string include)
        {

            var model = await _airMonitoringStationService.GetByIdAsync(id);
            var basePath = ControllerContext.HttpContext.Request.Path.Value;
            if (!String.IsNullOrEmpty(include))
            {
                var validIncludes = ResoursePathResolver
                    .GetValidQueryIncludes(ControllerPath, include.Trim().Split(','));
                if (validIncludes.Length > 0)
                {
                    var includes = await GetIncludes(validIncludes, id);
                    return Ok(await CreateResponseObjectAsync(basePath, model, includes));
                }
            }
            return Ok(await CreateResponseObjectAsync(basePath, model));
        }


        [HttpGet]
        [Route("{id}/{*path}")]
        public async Task<ActionResult> GetPossibleInclude(string id, string path)
        {
            if (ResoursePathResolver.IsPathValid(ControllerPath, id, path))
            {
                var basePath = ControllerContext.HttpContext.Request.Path.Value;
                var includeObject = await GetIncludeObject(ResoursePathResolver.GetIncludeByPath(ControllerPath, path), id);
                return Ok(await CreateResponseIncludeObjectAsync(id, path, basePath, includeObject));
            }
            return BadRequest();
        }

        
    }
}
