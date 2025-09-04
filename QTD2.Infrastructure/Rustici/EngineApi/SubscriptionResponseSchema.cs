using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class SubscriptionResponseSchema
    {
        public List<SubscriptionSchema> Subscriptions{ get; set; }
        public string More{ get; set; }
       
    }
}
