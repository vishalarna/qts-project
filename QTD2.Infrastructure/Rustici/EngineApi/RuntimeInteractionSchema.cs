using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class RuntimeInteractionSchema
    {
        public string Id { get; set; }
        public Type Type { get; set; }
        public string Timestamp { get; set; }
        public string TimestampUtc { get; set; }
        public string Weighting { get; set; }
        public string LearnerResponse { get; set; }
        public string Result { get; set; }
        public string Latency { get; set; }
        public string Description { get; set; }
        public List<string> CorrectResponses { get; set; }
    }
    public enum Type
    {
        TrueFalse,
        Choice,
        FillIn,
        LongFillIn,
        Likert,
        Matching,
        Performance,
        Sequencing,
        Numeric,
        Other
    }
}
