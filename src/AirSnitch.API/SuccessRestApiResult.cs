using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Api
{
    public class SuccessRestApiResult : IActionResult
    {
        private readonly QueryResult _queryResult;

        public SuccessRestApiResult(QueryResult queryResult)
        {
            _queryResult = queryResult;
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult("This is a plain not formatter text")
            {
                StatusCode = StatusCodes.Status200OK,
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}