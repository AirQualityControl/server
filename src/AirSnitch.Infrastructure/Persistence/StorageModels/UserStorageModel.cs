using System.Collections.Generic;
using System.Linq;
using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal sealed class UserStorageModel
    {
        public string FirstName;

        public string LastName;

        public string NickName;

        public long ClientUserId;

        public string LanguageCode;

        public bool IsBot;

        public bool IsActive;
        public ClientInfoDbModel ClientInfo { get; set; }

        public List<AirMonitoringStationSummaryDbModel> AirMonitoringStationSummaryList;

        public User MapToDomainModel()
        {
            var stations = AirMonitoringStationSummaryList?.Select(s => s.MapToDomainModel()).ToList();
            
            return new User(clientUserId: ClientUserId, monitoringStationSummaries: stations)
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                NickName = this.NickName,
                IsBot = this.IsBot,
                IsActive = this.IsActive,
                ClientInfo = this.ClientInfo.MapToDomainModel(),
                Language = new Language()
                {
                    Code = this.LanguageCode
                }
            };
        }

        public static UserStorageModel CreateFromDomainModel(User userDomainModel)
        {
            return new UserStorageModel()
            {
                FirstName = userDomainModel.FirstName,
                LastName = userDomainModel.LastName,
                NickName = userDomainModel.NickName,
                ClientUserId = userDomainModel.ClientUserId,
                ClientInfo = new ClientInfoDbModel()
                {
                    Name = userDomainModel.ClientInfo.Name
                },
                IsBot = userDomainModel.IsBot,
                IsActive = userDomainModel.IsActive,
                LanguageCode = userDomainModel.Language?.Code,
                AirMonitoringStationSummaryList = userDomainModel.AirMonitoringStations?.Select(AirMonitoringStationSummaryDbModel.FromDomainModel).ToList()
            };
        }
    }

    internal sealed class ClientInfoDbModel
    {
        public string Name;

        public ClientInfo MapToDomainModel()
        {
            return new ClientInfo()
            {
                Name = Name
            };
        }
    }

    internal sealed class AirMonitoringStationSummaryDbModel
    {
        public string StationId;

        public string Address;

        public string CityName;

        public AirMonitoringStationSummary MapToDomainModel()
        {
            return new AirMonitoringStationSummary()
            {
                StationId = this.StationId,
                Address = this.Address,
                CityName = this.CityName
            };
        }

        public static AirMonitoringStationSummaryDbModel FromDomainModel(AirMonitoringStationSummary summary)
        {
            return new AirMonitoringStationSummaryDbModel()
            {
                StationId = summary.StationId,
                Address = summary.Address,
                CityName = summary.CityName
            };
        }
    }
}