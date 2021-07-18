using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Api.Rest.Resources.Relationship;
using AirSnitch.Api.Rest.Resources.SubscriptionPlan;
using AirSnitch.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace AirSnitch.Api.Extensions
{
    public static class GraphExtensions
    {
        public static void BuildApiResourceGraph(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(BuildGraph());
        }

        public static void AddApiResourceRegistry(this IServiceCollection serviceCollection)
        {
            var apiResourceRegistry = new ApiResourceRegistry();
            apiResourceRegistry.RegisterApiResource(new ApiUserResource());
            apiResourceRegistry.RegisterApiResource(new ClientApiResource());
            apiResourceRegistry.RegisterApiResource(new SubscriptionPlanApiResource());

            serviceCollection.AddSingleton<IApiResourceRegistry>(apiResourceRegistry);
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