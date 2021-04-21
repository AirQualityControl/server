using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace AirSnitch.Api.Infrastructure.Common
{
    public class RestApiResult : ActionResult
    {
        private readonly object _model;
        private readonly string _controllerRoute;
        private readonly Dictionary<string, object> _includes;
        public RestApiResult(object model, string controllerRoute, Dictionary<string, object> includes = null)
        {
            _model = model;
            _controllerRoute = controllerRoute;
            _includes = includes;
        }

        private Response CreateResponseObject(string basePath)
        {
            //Inject resourse path resolver
            //var resourseResolver = new ResourcePathResolver();
            //var resourses = resourseResolver.GetResourses(_controllerRoute);
            //resourses.Add("self", new Resourse { Path = "" });
            //Parallel.ForEach(resourses, (item) =>
            //{
            //    item.Value.Path = item.Value.Path.Insert(0, basePath);
            //});


           return new Response
           {
                //Links = resourses,
                Values = _model,
                Includes = _includes
           };
        }

        private async Task<Response> CreateResponseObjectAsync(string basePath)
        {
            return await Task.Run(() => CreateResponseObject(basePath));
        }

        public override async Task<ObjectResult> ExecuteResultAsync(ActionContext context)
        { 
            var basePath = context.HttpContext.Request.Path.Value;
            
            return new OkObjectResult(await CreateResponseObjectAsync(basePath));
        }

    }
}
