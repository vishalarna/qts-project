using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class RuntimeSchema
    {
        public string CompletionStatus { get; set; }
        public string Credit { get; set; }
        public string Entry { get; set; }
        public string Exit { get; set; }
        public LearnerPreferenceSchema LearnerPreference { get; set; }
        public string Location { get; set; }
        public string Mode { get; set; }
        public string ProgressMeasure { get; set; }
        public string ScoreScaled { get; set; }
        public string ScoreRaw { get; set; }
        public string ScoreMin { get; set; }
        public string ScoreMax { get; set; }
        public string TotalTime { get; set; }
        public string TimeTracked { get; set; }
        public RuntimeSuccessStatus RuntimeSuccessStatus { get; set; }
        public string SuspendData { get; set; }
        public List<CommentSchema> LearnerComments { get; set; }
        public List<CommentSchema> LmsComments { get; set; }
        public List<RuntimeInteractionSchema> RuntimeInteractions { get; set; }
        public List<RuntimeObjectiveSchema> RuntimeObjectives { get; set; }

    }
    public enum RuntimeSuccessStatus
    {
        UNKNOWN,
        PASSED,
        FAILED
    }
}
