using AirSnitch.Api.Infrastructure.Attributes;
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
    [Authorize(Policy = Policies.RequiredAdmin)]
    [ApiController]
    [IncludeResourse(ControllersRoutes.AirmonitoringStation)]
    [Route(ControllersRoutes.User)]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IResoursePathResolver resoursePathResolver,
            IUserService userService) : base(resoursePathResolver)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {

            limit = limit > 0 ? limit : 10;
            (var paginatedResult, var total) = await _userService.GetPaginated(limit, offset);
            return Ok(await CreatePaginativeResponseObjectAsync(limit, offset, total, paginatedResult));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetWithIncludes(string id, [FromQuery] string include)
        {

            var model = await _userService.GetByIdAsync(id);
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

        protected override async Task<Dictionary<string, object>> GetIncludes(string[] includes, string Id)
        {
            return await _userService.GetIncludes(includes, Id);
        }

        protected override async Task<object> GetIncludeObject(string include, string id)
        {
            return include switch
            {
                ControllersRoutes.AirmonitoringStation => await _userService.GetIncludedAirMonitoringStation(id),
                ControllersRoutes.AirPolution => await _userService.GetIncludedAirpolution(id),
                ControllersRoutes.City => await _userService.GetIncludedCity(id),
                ControllersRoutes.Dataprovider => await _userService.GetIncludedDataProvider(id),
                _ => throw new ArgumentException($"Incorrect include: {include}")
            };
        }
    }
}
