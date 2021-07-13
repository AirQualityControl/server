using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.Relationship;
using AirSnitch.Api.Rest.Resources.SubscriptionPlan;
using Microsoft.Extensions.DependencyInjection;

namespace AirSnitch.Api.Extensions
{
    public static class GraphExtensions
    {
        public static void BuildApiResourceGraph(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(BuildGraph());
        }

        private static DirectAcyclicGraph<IApiResourceMetaInfo> BuildGraph()
        {
            var graph = new DirectAcyclicGraph<IApiResourceMetaInfo>(2);

            var apiUserVertex = new RelatedVertex<IApiResourceMetaInfo>(new ApiUserResource());
            var clientVertex = new RelatedVertex<IApiResourceMetaInfo>(new ClientApiResource());
            var subscriptionPlanVertex = new RelatedVertex<IApiResourceMetaInfo>(new SubscriptionPlanApiResource());
            
            graph.AddDirectedEdge(
                apiUserVertex,
                clientVertex,
                new IncludeRelationship()
            );
            
            graph.AddDirectedEdge(
                apiUserVertex, 
                subscriptionPlanVertex, 
                new IncludeRelationship()
            );

            return graph;
        }
    }
}