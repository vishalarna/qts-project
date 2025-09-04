using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class ObjectiveSchema
    {
        public string Id { get; set; }
        public bool Primary { get; set; }
        public ScoreSchema Score { get; set; }
        public double ScoreMax { get; set; }
        public double ScoreMin { get; set; }
        public double ScoreRaw { get; set; }
        public double PreviousScoreScaled { get; set; }
        public double FirstScoreScaled { get; set; }
        public double ProgressMeasure { get; set; }
        public string FirstSuccessTimeStamp { get; set; }
        public ObjectiveCompletion ObjectiveCompletion { get; set; }
        public ObjectiveSuccess ObjectiveSuccess { get; set; }
        public PreviousObjectiveSuccess PreviousObjectiveSuccess { get; set; }
    }
    public enum ObjectiveCompletion
    {
        UNKNOWN,
        COMPLETED,
        INCOMPLETE
    }
    public enum ObjectiveSuccess
    {
        UNKNOWN,
        PASSED,
        FAILED
    }
    public enum PreviousObjectiveSuccess
    {
        UNKNOWN,
        PASSED,
        FAILED
    }
}
