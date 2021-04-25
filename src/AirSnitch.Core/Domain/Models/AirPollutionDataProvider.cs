using System;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// ADT that provides and information about air pollution data provider
    /// </summary>
    public class AirPollutionDataProvider
    {
        /// <summary>
        /// User friendly name of data provider
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url of original data provider web site
        /// </summary>
        public Uri WebSiteUri { get; set; }

        /// <summary>
        /// Data update frequency interval
        /// </summary>
        public TimeSpan DataUpdateInterval { get; set; }
    }
}