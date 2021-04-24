using System;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal sealed class AirPollutionDataProviderMetaInfoStorageModel
    {
        /// <summary>
        /// User friendly name of data provider
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url of original data provider web site
        /// </summary>
        public string WebSiteUri { get; set; }

        /// <summary>
        /// Data update frequency interval
        /// </summary>
        public TimeSpan DataUpdateInterval { get; set; }

        /// <summary>
        /// Unique tag of data provider
        /// </summary>
        public string Tag { get; set; }
    }
}