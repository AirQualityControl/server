﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace AirSnitch.Api.Models
{
    public class AirMonitoringStationDTO
    {
        /// <summary>
        /// Name of station assigned by station provider
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Station name that characterize it location. For instance street address
        /// </summary>
        [JsonProperty("localName")]
        public string LocalName { get; set; }

        /// <summary>
        /// Market that states whether station is active or not.
        /// Keep in mind that this property could be changes during a day
        /// depending on physical station state
        /// </summary>
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Station time zone
        /// </summary>
        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Concrete geolocation coordinate of monitoring station
        /// </summary>
        [JsonProperty("location")]
        public GeoLocationDTO Location { get; set; }
    }
}
