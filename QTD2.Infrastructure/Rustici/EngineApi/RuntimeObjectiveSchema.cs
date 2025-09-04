using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class RuntimeObjectiveSchema
    {
        public string Id { get; set; }
        public string ScoreScaled { get; set; }
        public string ScoreMin { get; set; }
        public string ScoreMax { get; set; }
        public string ScoreRaw { get; set; }
        public RuntimeObjectiveSuccessStatus RuntimeObjectiveSuccessStatus { get; set; }
        public RuntimeObjectiveCompletionStatus RuntimeObjectiveCompletionStatus { get; set; }
        public string ProgressMeasure { get; set; }
        public string Description { get; set; }
    }
    public enum RuntimeObjectiveSuccessStatus
    {
        UNKNOWN,
        PASSED,
        FAILED
    }
    public enum RuntimeObjectiveCompletionStatus
    {
        UNKNOWN,
        COMPLETED,
        INCOMPLETE,
        NOT_ATTEMPTED,
        BROWSED
    }
}
