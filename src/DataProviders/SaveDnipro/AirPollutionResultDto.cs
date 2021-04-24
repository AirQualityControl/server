using System.Collections.Generic;
using System.Linq;

namespace SaveDnipro
{
    public class AirPollutionResultDto
    {
        public string Status { get; set; }
        
        public string Phenomenon { get; set; }
        
        public Dictionary<string, int> History { get; set; }
    }
}