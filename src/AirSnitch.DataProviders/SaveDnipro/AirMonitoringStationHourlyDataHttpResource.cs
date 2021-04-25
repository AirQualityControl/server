using AirSnitch.Core.Infrastructure.Network.HTTP;

namespace SaveDnipro
{
    public class AirMonitoringStationHourlyDataHttpResource : IHttpResource
    {
        public string Name => "hourly_data.json";
        public HttpResourceFormat Format => HttpResourceFormat.Json;
    }
}