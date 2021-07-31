using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            if (_responseBody.IsEmpty)
            {
                await new NotFoundResult().ExecuteResultAsync(context);
                return;
            }

            var formatterResultAsString = _responseBody.Value;

            var objectResult = new ObjectResult(formatterResultAsString)
            {
                StatusCode = StatusCodes.Status200OK,
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}