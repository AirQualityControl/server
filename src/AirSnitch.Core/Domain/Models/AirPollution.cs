using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Core.Domain.Exceptions;

namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Object that represent aire pollution results that was returned from monitoring station.
    /// </summary>
    public class AirPollution : EmptyDomainModel<AirPollution>, IDomainModel<AirPollution>
    {
        private static List<AirPollutionRange> _airPolutionRanges = new List<AirPollutionRange>()
        {
            new AirPollutionRange()
            {
                StartValue = 0,
                EndValue = 50,
                Description = "✅ Якість повітря хороша.\n\n➡ Можна гуляти та займатися спортом без обмежень 🚴 🏀"
            },
            new AirPollutionRange()
            {
                StartValue = 51,
                EndValue = 100,
                Description =
                    "✅ Якість повітря є прийнятною.\n\n➡ Можна гуляти, займатись спортом та провітрювати приміщення, однак людям з респіраторними захворюваннями варто утриматися від прогулянок❗"
            },
            new AirPollutionRange()
            {
                StartValue = 101,
                EndValue = 150,
                Description = "🆘 Якість потвітря погана 🆘 \n" +
                              "Існує ризик для твого здоров’я❗❗Варто утриматися від прогулянок і занять спортом та уникати " +
                              "провітрювання приміщень. Люди чутливі до захворювань легеневих шляхів можуть відчувати ефект забрудненого повітря"
            },
            new AirPollutionRange()
            {
                StartValue = 151,
                EndValue = 200,
                Description = "🆘 ПОВІТРЯ НАДЗВИЧАЙНО ЗАБРУДНЕНЕ 🆘 \n" +
                              "Існує значний ризик для твого здоров'я❗❗Залишайся вдома та не виходь на вулицю без захисної " +
                              "маски Якість повітря є поганою. Варто утриматися від прогулянок та закрити вікна."
            },
        };

        public bool IsEmpty { get; set; }

        /// <summary>
        /// AQI value based on US EPA standard
        /// </summary>
        public int AqiusValue {get;set;}

        /// <summary>
        /// Property that indicate data time of current air pollution
        /// </summary>
        public DateTime MeasurementDateTime { get; set; }

        /// <summary>
        /// Monitoring station instance that provided result
        /// </summary>
        public AirMonitoringStation MonitoringStation { get; set; }
        
        /// <summary>
        /// Wind speed (m/s)
        /// </summary>
        public int WindSpeed {get;set;}
        
        /// <summary>
        /// humidity percentage value
        /// </summary>
        public int Humidity {get;set;}
        
        /// <summary>
        /// /temperature value. By default in Celsius
        /// </summary>
        public int Temperature {get;set;}

        /// <summary>
        /// Analyze received air pollution data according to Air Quality Index
        /// For details check: https://www.airnow.gov/aqi/aqi-basics/
        /// </summary>
        /// <returns>AirPollutionAnalyzerResult <cref="AirPollutionAnalyzerResult"></cref>
        /// </returns>
        public AirPollutionResult Analyze()
        {
            var currentAirPollutionRange = _airPolutionRanges
                .Single(range => range.StartValue <= AqiusValue && range.EndValue >= AqiusValue);

            return new AirPollutionResult()
            {
                CurrentPollutionValue = AqiusValue,
                HumanOrientedMessage = currentAirPollutionRange.Description
            };
        }
        
        /// <summary>
        /// Method that returns a deep copy of current CityAirPollution object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var copy = (AirPollution)this.MemberwiseClone();
            copy.MonitoringStation = (AirMonitoringStation)MonitoringStation.Clone();
            return copy;
        }
        
        public bool IsValid()
        {
            if (IsEmpty)
            {
                return true;
            }

            return AqiusValue > 0 && MeasurementDateTime > DateTime.MinValue && MonitoringStation != null;
        }

        public void Validate()
        {
            if (AqiusValue > 0 && MeasurementDateTime > DateTime.MinValue && MonitoringStation != null)
            {
                return;
            }

            throw new InvalidEntityStateException("Air pollution object is not valid.");
        }

        public bool Equals(AirPollution other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IsEmpty == other.IsEmpty && AqiusValue == other.AqiusValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AirPollution) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsEmpty, AqiusValue);
        }

        public override string ToString()
        {
            return $"AIQ value: {AqiusValue}, MeasurementDateTime: {MeasurementDateTime}";
        }
    }
}