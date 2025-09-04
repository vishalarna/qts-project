using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class WebRequestPayload
    {
        public string PayloadId { get; set; }
        public string SubscriptionId { get; set; }
        public TopicEnum Topic { get; set; }
        public string[] Subtopics { get; set; }
        public string TenantName { get; set; }
        public string TimeStamp { get; set; }
        public double BodyVersion { get; set; }
        public double MessageVersion { get; set; }
    }

    public class RegistrationChangedSchema : WebRequestPayload
    {
        public RegistrationSubscriptionSchema Body { get; set; }
        public Resources Resources { get; set; }
    }
}
