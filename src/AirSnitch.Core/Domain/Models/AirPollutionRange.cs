namespace AirSnitch.Core.Domain.Models
{
    /// <summary>
    /// AQI air pollution range
    /// </summary>
    public struct AirPollutionRange
    {
        /// <summary>
        /// Gets or sets range lover bound value
        /// </summary>
        public int StartValue { get; set; }

        /// <summary>
        /// Gets or sets range upper bound value
        /// </summary>
        public int EndValue { get; set; }

        /// <summary>
        /// Gets or sets range description according to AQI
        /// </summary>
        public string Description { get; set; }
    }
}
