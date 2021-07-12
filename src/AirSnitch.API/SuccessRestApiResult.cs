using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirSnitch.Api.Controllers;
using AirSnitch.Api.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api
{
    public class SuccessRestApiResult : IActionResult
    {
        private readonly RestResponseBody _responseBody;

        public SuccessRestApiResult(RestResponseBody responseBody)
        {
            _responseBody = responseBody;
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_responseBody.Value)
            {
                StatusCode = StatusCodes.Status200OK,
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}