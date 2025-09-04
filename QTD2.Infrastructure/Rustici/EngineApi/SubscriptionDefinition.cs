using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class SubscriptionDefinition
    {
        public TopicEnum Topic { get; set; }
        public List<SubTopicEnum> Subtopics { get; set; }
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public string AuthId { get; set; }
        public int TimeOutMS { get { return 5000; } }
        public AsyncModeEnum AsyncMode { get; set; }
        public bool StrictOrdering { get { return false; } }
        public int ExpireMS { get; set; }
        public int RetryAttempts { get; set; }
        public int RetryDelaySeconds { get; set; }
        public DateTime IgnoreBeforeDate { get; set; }
        public List<FilterSchema> Filters { get; set; }
    }
}
