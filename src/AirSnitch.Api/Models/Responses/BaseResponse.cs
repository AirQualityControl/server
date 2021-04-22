using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Responses
{
    public class BaseResponse
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
       
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int? Status { get; set; }
		
		[JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
		public string ErrorCode { get; set; }

		[JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
		public string Message { get; set; }

		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, IEnumerable<string>> Details { get; set; }

		[JsonProperty("stackTrace", NullValueHandling = NullValueHandling.Ignore)]
		public string StackTrace { get; set; }

		public Exception Exception
		{
			set
			{
				ErrorCode = value.GetType().Name;
				Status = 500;
				Message = value.Message;
				StackTrace = value.StackTrace;
			}
		}

		public BaseResponse()
		{
			Status = 200;
		}

		public BaseResponse(Exception e)
		{
			Exception = e;
		}
	}


}
