using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class FilterSchema
    {
        public Target Target { get; set; }
        public List<string> Matches { get; set; }
    }

    public enum Target
    {
        CourseId, 
        LearningStandard, 
        Tenant, 
        RegistrationId, 
        SubscriptionId
    }
}
