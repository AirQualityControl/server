namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// Represent result of air pollution analyzing procedure
    /// </summary>
    public class AirPollutionResult
    {
        /// <summary>
        /// Gets or sets current air pollution level according to AQI level
        /// </summary>
        public int CurrentPollutionValue { get; set; }

        /// <summary>
        ///  Gets or sets message that describe air pollution value in simple terms for regular user
        /// </summary>
        public string HumanOrientedMessage { get; set; }
    }
}