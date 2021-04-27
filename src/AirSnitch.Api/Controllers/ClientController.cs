
using AirSnitch.Api.Infrastructure.Authorization;
using AirSnitch.Api.Infrastructure.Services;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [Authorize(Policy = Policies.RequiredUser)]
    [Route(ControllersRoutes.Client)]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _dummyUserService;
        public ClientController(IClientService dummyUserService) 
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

        [HttpPost]
        [Route("revoke-api-key")]
        public async Task<ActionResult> RevokeApiKey()
        {
            Request.Headers.TryGetValue(ApiKeyConstants.HeaderName, out var apiKeyHeaderValues);
            await _dummyUserService.RevokeKey(apiKeyHeaderValues);
            return Ok();
        }
    }
}
