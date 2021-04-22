using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.Authorization;
using AirSnitch.Api.Infrastructure.Interfaces;
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
        private IDummyUserService _dummyUserService;
        public UserController(IResoursePathResolver resoursePathResolver,
            IDummyUserService dummyUserService) : base(resoursePathResolver)
        {
            _dummyUserService = dummyUserService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-api-key")]
        public async Task<ActionResult> SignUp([FromBody] UserDTO user)
        {
            var apiKey = await _dummyUserService.CreateAsync(user);

            return Ok(apiKey);
        }

        
        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {

            return Ok(await CreatePaginativeResponseObjectAsync(limit, offset, 20,
                new Dictionary<string, object>
                {
                    ["1"] = new UserDTO
                    {
                        Name = "First name LastName",
                        PhoneNumber = "3806554556555"
                    },
                    ["2"] = new UserDTO
                    {
                        Name = "First name2 LastName2",
                        PhoneNumber = "3802222222225"
                    },
                })
            );
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetWithIncludes(int id, [FromQuery] string include)
        {

            if (!String.IsNullOrEmpty(include))
            {
                var validIncludes = ResoursePathResolver
                    .GetValidQueryIncludes(ControllerPath, include.Trim().Split(','));
                if (validIncludes.Length > 0)
                {
                    return Ok(await CreateResponseObjectAsync(
                        ControllerContext.HttpContext.Request.Path.Value,
                        new UserDTO
                        {
                            Name = "First name2 LastName2",
                            PhoneNumber = "3802222222225"
                        },
                        GetIncludes(validIncludes, id))
                    );
                }
            }

            return Ok(await CreateResponseObjectAsync(
                    ControllerContext.HttpContext.Request.Path.Value,
                    new UserDTO
                    {
                        Name = "First name1 LastName1",
                        PhoneNumber = "3800000000025"
                    }));
        }

        [HttpGet]
        [Route("{id}/{*path}")]
        public async Task<ActionResult> GetPossibleInclude(int id, string path)
        {
            if (ResoursePathResolver.IsPathValid(ControllerPath, id, path))
            {
                return Ok(await CreateResponseIncludeObjectAsync(id,
                    path,
                    ControllerContext.HttpContext.Request.Path.Value,
                    GetIncludeObject(ResoursePathResolver.GetIncludeByPath(ControllerPath, path), id)));
            }
            return BadRequest();
        }

        protected override object GetIncludeObject(string include, int id)
        {
            return include switch
            {
                "airmonitoringstations" => new AirMonitoringStationDTO { IsActive = id % 2 == 0, LocalName = $"secondSttion{id}", Name = $"General name{id}" },
                "airpolution" => new AirPollutionDTO { Temperature = 20, AqiusValue = 80, Humidity = 20, Message = "All good", WindSpeed = 20 },
                "city" => new CityDTO { FriendlyName = $"TestCity{id}", State = $"testState{id}", Code = "1234", CountryCode = "UA" },
                "dataproviders" => new DataProviderDTO { Name = $"TestDataProvider{id}", WebSiteUri = new Uri("https://test.com") },
                //"airmonitoringstations/airpolution" => new AirPollutionDTO { Temperature = 20, AqiusValue = 80, Humidity = 20, Message = "All good", WindSpeed = 20 },
                //"airmonitoringstations/city" => new CityDTO { FriendlyName = $"TestCity{id}", State = $"testState{id}", Code = "1234", CountryCode = "UA" },
                //"airmonitoringstations/dataproviders" => new DataProviderDTO { Name = $"TestDataProvider{id}", WebSiteUri = new Uri("https://test.com") },
                _ => throw new ArgumentException($"Incorrect include: {include}"),
            };
        }
    }
}
