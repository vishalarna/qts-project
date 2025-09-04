using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class PingResponse
    {
        public string ApiMessage { get; set; }
        public string CurrentTime { get; set; }
        public string DatabaseMessage { get; set; }
    }
}
