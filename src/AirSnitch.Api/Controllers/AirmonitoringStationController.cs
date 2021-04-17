using AirSnitch.Api.Infrastructure.Common;
using AirSnitch.Api.Infrastructure.PathResolver;
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
    [Route(ControllersRoutes.AirmonitoringStation)]
    public class AirmonitoringStationController : ControllerBase
    {
        private Dictionary<string, object> GetIncludes(string[] includes)
        {
            Dictionary<string, object> result = new();
            for (int i = 0; i < includes.Length; i++)
            {
                switch (includes[i])
                {
                    case "airPolution":
                        result.Add(includes[i], new AirPollutionDTO { Temperature = 20, AqiusValue = 80, Humidity = 20, Message = "All good", WindSpeed = 20 });
                        break;
                    case "city":
                        result.Add(includes[i], new CityDTO { FriendlyName = $"TestCity{i}", State = $"testState{i}", Code = "1234", CountryCode = "UA"});
                        break;
                    case "dataProvider":
                        result.Add(includes[i], new DataProviderDTO { Name = $"TestDataProvider{i}", WebSiteUri = new Uri("https://test.com") });
                        break;
                    default:
                        throw new ArgumentException($"Incorrect include: {includes[i]}");
                }
            }
            return result;
        }

        private Response CreateResponseObject(object model, Dictionary<string, object> includes = null)
        {
            //Inject resourse path resolver
            var resourseResolver = new ResourcePathResolver();
            var resourses = resourseResolver.GetResourses(ControllersRoutes.AirmonitoringStation);
            resourses.Add("self", new Resourse { Path = "" });
            Parallel.ForEach(resourses, (item) =>
            {
                item.Value.Path = item.Value.Path.Insert(0, ControllerContext.HttpContext.Request.Path.Value);
            });

            return new Response
            {
                Links = resourses,
                Values = model,
                Includes = includes
            };
        }

        private async Task<Response> CreateResponseObjectAsync(object model, Dictionary<string, object> includes = null)
        {
            return await Task.Run(() => CreateResponseObject(model, includes));
        }

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
                return Ok(await CreateResponseObjectAsync(
                    new AirMonitoringStationDTO {
                        IsActive = true, LocalName = "firstSttion", Name = "General name"
                    },
                    GetIncludes(include.Trim().Split(',')))
                );
            }

            return Ok(await CreateResponseObjectAsync(
                    new AirMonitoringStationDTO
                    {
                        IsActive = true,
                        LocalName = "firstSttion",
                        Name = "General name"
                    }));
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
