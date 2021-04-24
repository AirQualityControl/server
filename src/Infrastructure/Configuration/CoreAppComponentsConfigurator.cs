using System.Collections.Generic;
using AirSnitch.Core.Infrastructure;
using AirSnitch.Core.Infrastructure.Configuration;

namespace AirSnitch.Infrastructure.Configuration
{
    /// <summary>
    /// Class that prepare app for start, configuring different app components.
    /// </summary>
    public class CoreAppComponentsConfigurator : IAppComponentConfigurator
    {
        private readonly IEnumerable<ISystemComponent> _systemComponents;

        public CoreAppComponentsConfigurator(IEnumerable<ISystemComponent> systemComponents)
        {
            _systemComponents = systemComponents;
        }
        
        public void PrepareAppComponents()
        {
            foreach (var component in _systemComponents)
            {
                component.CheckComponent();
            }
        }
    }
}