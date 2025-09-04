using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class LaunchLinkRequestSchema
    {
        public int Expiry { get; set; }
        public string RedirectOnExitUrl { get; set; }
        public string Tracking { get; set; }
        public string StartSco { get; set; }
        public ItemValuePairSchema AdditionalValues { get; set; }
    }
}
