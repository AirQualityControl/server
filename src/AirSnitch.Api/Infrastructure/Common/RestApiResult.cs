using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AirSnitch.Api.Infrastructure.Common
{
    public class RestApiResult<T> : ActionResult
    {
        private readonly T _model;
        private readonly string _controllerRoute;
        private readonly Dictionary<string, object> _includes;
        public RestApiResult(T model, string controllerRoute, Dictionary<string, object> includes = null)
        {
            _model = model;
            _controllerRoute = controllerRoute;
            _includes = includes;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {

            var resourseResolver = new ResourcePathResolver();

            var basePath = context.HttpContext.Request.Path.Value;

            var resourses = resourseResolver.GetPathResourses(_controllerRoute);
            resourses.Add("self", new Resourse { Path = "" });
            Parallel.ForEach(resourses, (item) =>
            {
                item.Value.Path = item.Value.Path.Insert(0, basePath);
            });
            
            
            Response<T> result = new Response<T>
            {
                Links = resourses,
                Values = _model,
                Includes = _includes
            };
            
            return Task.FromResult(result);
        }

    }
}
