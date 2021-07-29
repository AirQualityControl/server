namespace AirSnitch.Api.Rest.ResponseBodyFormatters
{
    /// <summary>
    /// Interface that represent a contract for every response formatter in RESTful API
    /// </summary>
    public interface IResponseBodyFormatter
    {
        /// <summary>
        /// Format response and return a result as a string
        /// </summary>
        /// <param name="responseBody">Response body that needs to be formatted</param>
        /// <returns></returns>
        string FormatResponse(object responseBody);
    }
}