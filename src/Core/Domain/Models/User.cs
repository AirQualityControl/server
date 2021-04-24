using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Core.Domain.Exceptions;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Class that represent application user
    /// </summary>
    public class User : EmptyDomainModel<User>, IDomainModel<User>
    {
        private List<AirMonitoringStationSummary> _monitoringStationSummaries;
        private const int MonitoringStationsLimit = 3;
        private readonly object _syncObj = new object();
        
        public User()
        {
            
        }
        
        public User(long clientUserId, List<AirMonitoringStationSummary> monitoringStationSummaries = null)
        {
            ClientUserId = clientUserId;
            if (monitoringStationSummaries != null)
            {
                _monitoringStationSummaries = monitoringStationSummaries.ToList();
            }
        }

        /// <summary>
        ///     First name of user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     User nickName that is specific for platform
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     Unique identifier of user in different platforms.
        /// </summary>
        public long ClientUserId { get; }

        /// <summary>
        ///     User language settings
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        ///     Mark that specify is user a bot or not
        /// </summary>
        public bool IsBot { get; set; }

        /// <summary>
        ///     Define weather user active or not.
        ///     Default value is true, witch means that users is active by default.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        ///     Return information about user client cref="ClientInfo">.
        ///     For example concrete messenger platform(Telegram, Facebook Messenger)
        /// </summary>
        public ClientInfo ClientInfo { get; set; }

        /// <summary>
        ///     Represents a collection of AirMonitoringStationSummary that are being monitored be current user.
        ///     If user does not enable monitoring null will be returned.
        /// </summary>
        public IReadOnlyCollection<AirMonitoringStationSummary> AirMonitoringStations =>
            _monitoringStationSummaries;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="airMonitoringStation"></param>
        /// <returns></returns>
        public StartAirMonitoringRequestResult StartAirMonitoringForStation(AirMonitoringStationSummary airMonitoringStation)
        {
            lock (_syncObj)
            {
                if (IsNumberOfMonitoringStationExceededLimit())
                {
                    return new StartAirMonitoringRequestResult(FailureReason.NumberOfStationsExceededLimit);
                }

                if (IsStationAlreadyBeingMonitored(airMonitoringStation))
                {
                    return new StartAirMonitoringRequestResult(FailureReason.StationAlreadyMonitored);
                }
                
                AddNewStationForMonitoring(airMonitoringStation);
                return new StartAirMonitoringRequestResult(FailureReason.None);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="airMonitoringStation"></param>
        public void CancelAirMonitoringForStation(AirMonitoringStationSummary airMonitoringStation)
        {
            RemoveStationFromMonitoring(airMonitoringStation);
        }

        /// <summary>
        ///     Methods that verify whether number of monitoring stations is exceeded limits
        /// </summary>
        /// <returns>Returns true in case if station exceeded limits. False - otherwise</returns>
        private bool IsNumberOfMonitoringStationExceededLimit()
        {
            if (_monitoringStationSummaries == null)
            {
                return false;
            }
            return !(_monitoringStationSummaries.Count < MonitoringStationsLimit);
        }
        
        /// <summary>
        ///     Methods that verify whether station is already monitored by user
        /// </summary>
        /// <returns>Returns true in case if station monitoring enabled. False if not</returns>
        private bool IsStationAlreadyBeingMonitored(AirMonitoringStationSummary stationSummary)
        {
            lock (_syncObj)
            {
                return _monitoringStationSummaries != null 
                       && _monitoringStationSummaries.Contains(stationSummary);
            }
        }

        /// <summary>
        ///     Methods that verify whether station is already monitored by user
        /// </summary>
        /// <param name="stationId">Station id</param>
        /// <returns>Returns true in case if station monitoring enabled. False if not</returns>
        public bool IsStationAlreadyBeingMonitored(string stationId)
        {
            lock (_syncObj)
            {
                return _monitoringStationSummaries != null 
                       && _monitoringStationSummaries.Any(s => s.StationId == stationId);
            }
        }

        /// <summary>
        ///     Add new station for current user.
        /// </summary>
        /// <param name="stationSummary">AirMonitoringStationSummary that will be added.</param>
        /// <throws>ContractViolationException cref="Core.Infrastructure.Contracts.ContactViolationException"></throws>
        /// Precondition:
        ///     stationSummary is not null
        private void AddNewStationForMonitoring(AirMonitoringStationSummary stationSummary)
        {
            Require.That(stationSummary, Is.NotNull);
            lock(_syncObj)
            {
                if (_monitoringStationSummaries != null)
                {
                    _monitoringStationSummaries.Add(stationSummary);
                }
                else
                {
                    _monitoringStationSummaries = new List<AirMonitoringStationSummary>{ stationSummary };
                }
            }
        }

        /// <summary>
        ///     Return user's display name
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            return !String.IsNullOrEmpty(FirstName) ? FirstName : NickName;
        }

        /// <summary>
        ///     Remove specific station form monitoring
        /// </summary>
        /// <param name="stationSummary">AirMonitoringStationSummary that will be removed from monitoring.</param>
        /// <throws>ContractViolationException cref="Core.Infrastructure.Contracts.ContactViolationException"></throws>
        /// Precondition:
        ///     stationSummary is not null
        private void RemoveStationFromMonitoring(AirMonitoringStationSummary stationSummary)
        {
            Require.That(stationSummary, Is.NotNull);
            lock (_syncObj)
            {
                _monitoringStationSummaries?.Remove(stationSummary);
            }
        }

        /// <summary>
        ///     Deactivate current user, marking his internal state as inactive
        /// </summary>
        public void Deactivate()
        {
            lock (_syncObj)
            {
                //TODO: rethink this logic
                _monitoringStationSummaries?.Clear();
                IsActive = false;
            }
        }

        /// <summary>
        ///     Activate current user, marking his internal state as active
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        ///<inheritdoc/>
        public bool IsEmpty { get; set; }

        ///<inheritdoc/>
        public bool IsValid()
        {
            //TODO: complete this logic
            return true;
        }

        ///<inheritdoc/>
        public void Validate()
        {
            if (ClientUserId == default && String.IsNullOrEmpty(FirstName))
            {
                throw new InvalidDomainModelStateException();
            }
        }
        
        ///<inheritdoc/>
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ClientUserId == other.ClientUserId;
        }

        public override int GetHashCode()
        {
            return ClientUserId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        ///<inheritdoc/>
        public object Clone()
        {
            //TODO:https://github.com/AirSnitch/AirSnitchBot/issues/286
            throw new NotImplementedException();
        }
        
        public override string ToString()
        {
            return $"ID :{FirstName}, Name : {FirstName}, NickName: {NickName}";
        }
    }

    public class StartAirMonitoringRequestResult
    {
        public StartAirMonitoringRequestResult(FailureReason failureReason)
        {
            FailureReason = failureReason;
        }

        public bool IsSuccess => FailureReason == FailureReason.None;

        public FailureReason FailureReason { get; }
    }

    public enum FailureReason
    {
        None,
        NumberOfStationsExceededLimit,
        StationAlreadyMonitored,
        InternalException
    }
}
