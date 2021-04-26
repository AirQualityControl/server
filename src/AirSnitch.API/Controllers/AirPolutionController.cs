using AirSnitch.Api.Infrastructure.Authorization;
using AirSnitch.Core.UseCases.GetAirPollution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;
using System.Diagnostics.Contracts;
using AirSnitch.Api.Models;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Models.Responses;

namespace AirSnitch.Api.Controllers
{
    [Authorize(Policy = Policies.RequiredUser)]
    [ApiController]
    [Route(ControllersRoutes.AirPolution)]
    public class AirPolutionController : BaseApiController
    {
        private readonly IGetAirPollutionByGeoLocationUseCase _getAirPollutionByGeoLocationUseCase;

        public AirPolutionController(IResoursePathResolver resoursePathResolver,
            IGetAirPollutionByGeoLocationUseCase getAirPollutionByGeoLocationUseCase) : base(resoursePathResolver)
        {
            _getAirPollutionByGeoLocationUseCase = getAirPollutionByGeoLocationUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<Response<AirPollutionDTO>>> Get(double lat, double lng)
        {
            var useCaseResult = await _getAirPollutionByGeoLocationUseCase.ExecuteAsync(
                new GeoLocation
                {
                    Latitude = lat,
                    Longitude = lng
                });
            if (useCaseResult.IsSuccess)
            {
                var airPollution = useCaseResult.Result;
                if (!airPollution.IsEmpty)
                {
                    return Ok(await CreateResponseObjectAsync(
                        ControllerContext.HttpContext.Request.Path.Value,
                        new AirPollutionDTO
                        {
                            AqiusValue = airPollution.AqiusValue,
                            Humidity = airPollution.Humidity,
                            MeasurementDateTime = airPollution.MeasurementDateTime,
                            Temperature = airPollution.Temperature,
                            WindSpeed = airPollution.WindSpeed,
                            Message =
                               $"👉 Індекс якості повітря: *{airPollution.AqiusValue} \n\n* " +
                               $"{airPollution.Analyze().HumanOrientedMessage} \n\n" +
                               $"Дані зібрані зі станції у місті *{airPollution.MonitoringStation.City}*, за адресою *{airPollution.MonitoringStation.Name}*"
                        })); 
                }
            }

            return Ok(await CreateResponseObjectAsync(
                        ControllerContext.HttpContext.Request.Path.Value,
                        new AirPollutionDTO
                        {
                            Message = $"На данний момент я не зміг знайти інформацію про забрудненню повітря навколо тебе." +
                                $"Спробуй, будь ласка, пізніше"
                        })); 
        }

        protected override Task<object> GetIncludeObject(string include, string id)
        {
            throw new ArgumentException($"Incorrect include: {include}");
        }
    }
}
