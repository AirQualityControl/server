using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.ApiUser;
using AirSnitch.Api.Resources.Client;
using AirSnitch.Api.Resources.Relationship;
using AirSnitch.Api.Resources.SubscriptionPlan;
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