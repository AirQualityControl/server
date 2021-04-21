using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.Common;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [IncludeResourse(ControllersRoutes.City)]
    [IncludeResourse(ControllersRoutes.Dataprovider)]
    [IncludeResourse(ControllersRoutes.AirPolution)]
    [Route(ControllersRoutes.AirmonitoringStation)]
    public class AirMonitoringStationController : BaseApiController
    {
        public AirMonitoringStationController(IResoursePathResolver resoursePathResolver) : base(resoursePathResolver)
        { }
        

        protected override object GetIncludeObject(string include, int id)
        {
            return include switch
            {
                "airpolution" => new AirPollutionDTO { Temperature = 20, AqiusValue = 80, Humidity = 20, Message = "All good", WindSpeed = 20 },
                "city" => new CityDTO { FriendlyName = $"TestCity{id}", State = $"testState{id}", Code = "1234", CountryCode = "UA" },
                "dataproviders" => new DataProviderDTO { Name = $"TestDataProvider{id}", WebSiteUri = new Uri("https://test.com") },
                _ => throw new ArgumentException($"Incorrect include: {include}"),
            };
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {

            return Ok(await CreatePaginativeResponseObjectAsync(limit, offset, 20,
                new Dictionary<string, object> { 
                    ["1"] = new AirMonitoringStationDTO { 
                        IsActive = true,
                        LocalName = "firstSttion",
                        Name = "General name"
                    },
                    ["2"] = new AirMonitoringStationDTO
                    {
                        IsActive = false,
                        LocalName = "secondSttion",
                        Name = "General name2"
                    }
                })
            );
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetWithIncludes(int id, [FromQuery] string include)
        {
            
            if (!String.IsNullOrEmpty(include))
            {
                return Ok(await CreateResponseObjectAsync(
                    ControllerContext.HttpContext.Request.Path.Value,
                    new AirMonitoringStationDTO {
                        IsActive = true, LocalName = "firstSttion", Name = "General name"
                    },
                    GetIncludes(include.Trim().Split(','), id))
                );
            }

            return Ok(await CreateResponseObjectAsync(
                    ControllerContext.HttpContext.Request.Path.Value,
                    new AirMonitoringStationDTO
                    {
                        IsActive = true,
                        LocalName = "firstSttion",
                        Name = "General name"
                    }));
        }


        [HttpGet]
        [Route("{id}/{*path}")]
        public async Task<ActionResult> GetPossibleInclude(int id, string path)
        {
            if (ResoursePathResolver.IsQueryPathValid(ControllerPath, id, path))
            {
                return Ok(await CreateResponseIncludeObjectAsync(id,
                    path,
                    ControllerContext.HttpContext.Request.Path.Value,
                    GetIncludeObject(path, id)));
            }
            return BadRequest();
        }

        
    }
}
