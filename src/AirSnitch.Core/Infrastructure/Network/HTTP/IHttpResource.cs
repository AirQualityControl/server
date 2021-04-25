namespace AirSnitch.Core.Infrastructure.Network.HTTP
{
    /// <summary>
    /// Interface that represent http resource
    /// </summary>
    public interface IHttpResource
    {
        /// <summary>
        /// Resource name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Resource format type.For example JSON or XML.
        /// </summary>
        HttpResourceFormat Format { get; }
    }

    /// <summary>
    /// Enum that specify supported types of Http resources.
    /// </summary>
    public enum HttpResourceFormat
    {
        /// <summary>
        /// JSON format 
        /// </summary>
        Json,
        /// <summary>
        /// XML format
        /// </summary>
        Xml,
    }
}