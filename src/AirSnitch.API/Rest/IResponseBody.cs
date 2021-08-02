using System.Net.Mime;
using AirSnitch.Api.Rest.ResponseBodyFormatters;

namespace AirSnitch.Api.Rest
{
    public interface IResponseBody
    {
        string Value { get; }

        bool IsEmpty { get; }

        ContentType ContentType { get; }
    }
}