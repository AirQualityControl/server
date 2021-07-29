using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AirSnitch.Api.Rest.ResponseBodyFormatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace AirSnitch.Api.Rest
{
    public class RestApiResult : IActionResult
    {
        private readonly IResponseBody _responseBody;

        public RestApiResult(IResponseBody responseBody)
        {
            _responseBody = responseBody;
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var formatterResultAsString = _responseBody.Value;

            var objectResult = new ObjectResult(formatterResultAsString)
            {
                StatusCode = StatusCodes.Status200OK,
                //ContentTypes = new MediaTypeCollection(){new MediaTypeHeaderValue(_responseBody.ContentType.ToString())}
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}