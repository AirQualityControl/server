using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using AirSnitch.Api.Models;
using AirSnitch.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        private string _controllerPath;

        public BaseApiController(IResoursePathResolver resoursePathResolver)
        {
            ResoursePathResolver = resoursePathResolver;
        }

        protected IResoursePathResolver ResoursePathResolver { get; private set; }

        protected abstract object GetIncludeObject(string include, int id);

        protected virtual string ControllerPath => _controllerPath ??= ControllerContext.ActionDescriptor.ControllerTypeInfo.CustomAttributes
                .Single(item => item.AttributeType == typeof(RouteAttribute)).ConstructorArguments[0].Value as string;

        protected virtual Dictionary<string, object> GetIncludes(string[] includes, int Id)
        {
            Dictionary<string, object> result = new();
            for (int i = 0; i < includes.Length; i++)
            {
                result.Add(includes[i], GetIncludeObject(includes[i], Id));
            }
            return result;
        }

        protected virtual Response<T> CreateResponseObject<T>(string basePath, T model, Dictionary<string, object> includes = null)
        {
            var cachedResourses = ResoursePathResolver.GetResourses(ControllerPath);

            var resourses = new Dictionary<string, Resourse>();
            Parallel.ForEach(cachedResourses, (item) =>
            {
                resourses.Add(item.Key, new Resourse { Path = item.Value.Path.Insert(0, basePath) });
            });

            return new Response<T>
            {
                Links = resourses,
                Values = model,
                Includes = includes
            };
        }

        protected virtual Response<T> CreateResponseIncludeObject<T>(int id, string includeKey, string basePath, T model, Dictionary<string, object> includes = null)
        {
            var cachedResourses = ResoursePathResolver.GetResourses(ControllerPath);
            //basePath.LastIndexOf(ControllerPath+$"/{id}/")
            string pathBeforeInclude = basePath.Remove(basePath.LastIndexOf("/" + includeKey)).TrimEnd();

            string currentInclude = basePath.Remove(0, basePath.LastIndexOf('/') + 1);

            var resourses = new Dictionary<string, Resourse>();
            Parallel.ForEach(cachedResourses, (item) =>
            {
                if (item.Key == "self")
                {
                    resourses.Add(ControllerPath, new Resourse { Path = item.Value.Path.Insert(0, pathBeforeInclude) });
                }
                else if (item.Key == currentInclude)
                {
                    resourses.Add("self", new Resourse { Path = item.Value.Path.Insert(0, pathBeforeInclude) });
                }
                else
                {
                    resourses.Add(item.Key, new Resourse { Path = item.Value.Path.Insert(0, pathBeforeInclude) });
                }

            });

            return new Response<T>
            {
                Links = resourses,
                Values = model,
                Includes = includes
            };
        }

        protected virtual PaginativeResponse<T> CreatePaginativeResponseObject<T>(int limit, int offset, int total,
            Dictionary<string, T> models, Dictionary<string, object> includes = null)
        {
            limit = limit == 0 ? 2 : limit;
            string requestPath = ControllerContext.HttpContext.Request.Path.Value;
            string format = requestPath + "?limit={0}&offset={1}";

            int maxOffset = total - limit;

            Dictionary<string, Resourse> links = new()
            {
                ["self"] = new Resourse { Path = String.Format(format, limit, offset) }
            };
            if (offset + limit <= maxOffset)
            {
                links.Add("next", new Resourse { Path = String.Format(format, limit, offset + limit) });
            }
            if (offset <= maxOffset)
            {
                links.Add("last", new Resourse { Path = String.Format(format, limit, maxOffset) });
            }
            if (offset > 0)
            {
                links.Add("first", new Resourse { Path = String.Format(format, limit, 0) });
            }

            List<Response<T>> items = new();
            foreach (var item in models)
            {
                items.Add(CreateResponseObject(requestPath + "/" + item.Key, item.Value, includes));
            }

            return new PaginativeResponse<T>
            {
                Responses = items,
                Count = models.Count,
                Total = total,
                Offset = offset,
                Links = links
            };
        }

        protected virtual async Task<Response<T>> CreateResponseIncludeObjectAsync<T>(int id, string includeKey, string basePath,
            T model, Dictionary<string, object> includes = null)
        {
            return await Task.Run(() => CreateResponseIncludeObject(id, includeKey, basePath, model, includes));
        }

        protected virtual async Task<PaginativeResponse<T>> CreatePaginativeResponseObjectAsync<T>(int limit, int offset, int total,
            Dictionary<string, T> models, Dictionary<string, object> includes = null)
        {
            return await Task.Run(() => CreatePaginativeResponseObject(limit, offset, total, models, includes));
        }

        protected virtual async Task<Response<T>> CreateResponseObjectAsync<T>(string basePath, T model, Dictionary<string, object> includes = null)
        {
            return await Task.Run(() => CreateResponseObject(basePath, model, includes));
        }
    }
}
