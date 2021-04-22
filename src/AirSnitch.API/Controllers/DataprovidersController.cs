﻿using AirSnitch.Api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    [Authorize(Policy = Policies.RequiredUser)]
    [ApiController]
    [Route(ControllersRoutes.Dataprovider)]
    public class DataprovidersController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return await Task.FromResult(Ok($"Dataprovider stub for id: {id}"));
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int limit, int offset)
        {
            return await Task.FromResult(Ok($"Dataprovider collection stub limit: {limit}, offset: {offset}"));
        }
    }
}
