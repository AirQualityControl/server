namespace AirSnitch.Core.Infrastructure
{
    /// <summary>
    /// Class that represent interface for accessing application configuration
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// Return value of T from app config by specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string Get(string key);
    }
}