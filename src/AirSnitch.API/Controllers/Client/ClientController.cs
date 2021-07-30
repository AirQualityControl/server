using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Registry;

namespace AirSnitch.Api.Controllers.Client
{
    public class ClientController : RestApiController
    {
        public ClientController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, IApiResourceRegistry apiResourceRegistry) : base(apiResourcesGraph, apiResourceRegistry)
        {
        }

        protected override IApiResourceMetaInfo CurrentResource { get; }
    }
}