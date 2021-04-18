﻿using AirSnitch.Api.Infrastructure.Common;
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
        private ResourcePathResolver _resourseResolver;
        public AirmonitoringStationController()
        {
            //Inject resourse path resolver
            _resourseResolver = new ResourcePathResolver();
        }

        private Dictionary<string, object> GetIncludes(string[] includes, int stationId)
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

        private Response CreateResponseObject(string basePath, object model, Dictionary<string, object> includes = null)
        {
            var cachedResourses = _resourseResolver.GetResourses(ControllersRoutes.AirmonitoringStation);
            
            var resourses = new Dictionary<string, Resourse>();
            Parallel.ForEach(cachedResourses, (item) =>
            {
                resourses.Add(item.Key, new Resourse { Path = item.Value.Path.Insert(0, basePath) });
            });

            return new Response
            {
                Links = resourses,
                Values = model,
                Includes = includes
            };
        }

        private PaginativeResponse CreatePaginativeResponseObject(int limit, int offset, int total, 
            Dictionary<string, object> models, Dictionary<string, object> includes = null)
        {
            
            string basePath = ControllerContext.HttpContext.Request.Path.Value;
            string format = basePath + "?limit={0}&offset={1}";
            
            int maxOffset = total - limit;

            Dictionary<string, Resourse> links = new()
            {
                ["self"] = new Resourse { Path = String.Format(format,limit, offset)}
            };
            if(offset + limit <= maxOffset)
            {
                links.Add("next", new Resourse { Path = String.Format(format, limit, offset + limit) });
            }
            if (offset <= maxOffset)
            {
                links.Add("last", new Resourse { Path = String.Format(format, limit, maxOffset)});
            }
            if (offset > 0)
            {
                links.Add("first", new Resourse { Path = String.Format(format, limit, 0) });
            }

            List<Response> items = new();
            foreach(var item in models)
            {
                items.Add(CreateResponseObject(basePath + "/" + item.Key, item.Value, includes));
            }
            
            return new PaginativeResponse
            {
                Responses = items,
                Count = models.Count,
                Total = total,
                Offset = offset,
                Links = links
            };
        }

        private async Task<PaginativeResponse> CreatePaginativeResponseObjectAsync(int limit, int offset, int total,
            Dictionary<string, object> models, Dictionary<string, object> includes = null)
        {
            return await Task.Run(() => CreatePaginativeResponseObject(limit, offset, total, models, includes));
        }

        private async Task<Response> CreateResponseObjectAsync(string basePath, object model, Dictionary<string, object> includes = null)
        {
            return await Task.Run(() => CreateResponseObject(basePath, model, includes));
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
