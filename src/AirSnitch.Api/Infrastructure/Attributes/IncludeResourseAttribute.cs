using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IncludeResourseAttribute : Attribute
    {
        public string Name { get; private set; }

        public IncludeResourseAttribute(string name)
        {
            Name = name;
        }
    }
}
