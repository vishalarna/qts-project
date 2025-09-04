using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class ActivityResultSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Attempts { get; set; }
        public ActivityCompletion ActivityCompletion { get; set; }
        public ActivitySuccess ActivitySuccess { get; set; }
        public ScoreSchema Score { get; set; }
        public string timeTracked { get; set; }
        public CompletionAmountSchema CompletionAmount { get; set; }
        public bool Suspended { get; set; }
        public List<ObjectiveSchema> Objectives { get; set; }
        public StaticPropertiesSchema StaticProperties { get; set; }
        public RuntimeSchema Runtime { get; set; }
        public List<ActivityResultSchema> Children { get; set; }
    }

    public enum ActivityCompletion
    {
        UNKNOWN,
        COMPLETED,
        INCOMPLETE
    }
    public enum ActivitySuccess
    {
        UNKNOWN,
        PASSED,
        FAILED
    }
}
