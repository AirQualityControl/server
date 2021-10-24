using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.AirPollution;
using AirSnitch.Api.Rest.Resources.AirQualityIndex;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.DataProvider;
using AirSnitch.Api.Rest.Resources.MonitoringStation;
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
            apiResourceRegistry.RegisterApiResource(new AirQualityIndexResource());
            apiResourceRegistry.RegisterApiResource(new ApiUserResource());
            apiResourceRegistry.RegisterApiResource(new ClientApiResource());
            apiResourceRegistry.RegisterApiResource(new SubscriptionPlanApiResource());
            apiResourceRegistry.RegisterApiResource(new MonitoringStationResource());
            apiResourceRegistry.RegisterApiResource(new AirPollutionResource());
            apiResourceRegistry.RegisterApiResource(new DataProviderApiResource());

            serviceCollection.AddSingleton<IApiResourceRegistry>(apiResourceRegistry);
        }

        private static DirectAcyclicGraph<IApiResourceMetaInfo> BuildGraph()
        {
            var graph = new DirectAcyclicGraph<IApiResourceMetaInfo>(2);

            var apiUserVertex = new RelatedVertex<IApiResourceMetaInfo>(new ApiUserResource());
            var clientVertex = new RelatedVertex<IApiResourceMetaInfo>(new ClientApiResource());
            var subscriptionPlanVertex = new RelatedVertex<IApiResourceMetaInfo>(new SubscriptionPlanApiResource());
            var monitoringStationVertex = new RelatedVertex<IApiResourceMetaInfo>(new MonitoringStationResource());
            var airQualityIndexVertex = new RelatedVertex<IApiResourceMetaInfo>(new AirQualityIndexResource());
            var airPollutionVertex = new RelatedVertex<IApiResourceMetaInfo>(new AirPollutionResource());
            var dataProviderVertex = new RelatedVertex<IApiResourceMetaInfo>(new DataProviderApiResource());
            
            
            graph.AddDirectedEdge(monitoringStationVertex, airQualityIndexVertex, new IncludeRelationship());
            graph.AddDirectedEdge(monitoringStationVertex, airPollutionVertex, new IncludeRelationship());
            graph.AddDirectedEdge(monitoringStationVertex, dataProviderVertex, new IncludeRelationship());
            
            
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