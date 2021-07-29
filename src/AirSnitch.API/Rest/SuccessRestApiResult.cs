using System.Threading.Tasks;
using AirSnitch.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api
{
    public class SuccessRestApiResult : IActionResult
    {
        private readonly IResponseBody _responseBody;

        public SuccessRestApiResult(IResponseBody responseBody)
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