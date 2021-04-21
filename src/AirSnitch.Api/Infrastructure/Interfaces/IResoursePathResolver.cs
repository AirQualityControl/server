﻿using AirSnitch.Api.Infrastructure.PathResolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Interfaces
{
    public interface IResoursePathResolver
    {
        Dictionary<string, Resourse> GetResourses(string controllerPath);
        bool IsQueryPathValid(string controllerPath, int id, string queryPath);
    }
}
