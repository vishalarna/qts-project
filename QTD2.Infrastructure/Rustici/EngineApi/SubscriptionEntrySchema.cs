using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class SubscriptionEntrySchema
    {
        public string Id { get; set; }
        public DateTime LastUpdate { get; set; }
        public SubscriptionDefinition Definition { get; set; }
        public string LastExceptionReference { get; set; }
        public DateTime LastExceptionDate { get; set; }
    }
}
