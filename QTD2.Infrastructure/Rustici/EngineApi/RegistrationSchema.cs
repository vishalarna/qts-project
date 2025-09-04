using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class RegistrationSchemaResponse
    {
        public RegistrationSchema Data { get; set; }
    }

    public class RegistrationSchema
    {
        public string Id { get; set; }
        public int Instance { get; set; }
        public string XapiRegistrationId { get; set; }
        public string Lti13SubjectIdentifier { get; set; }
        public string DispatchId { get; set; }
        public DateTime Updated { get; set; }
        public RegistrationCompletion RegistrationCompletion { get; set; }
        public RegistrationSuccess RegistrationSuccess { get; set; }
        public ScoreSchema Score { get; set; }
        public double TotalSecondsTracked { get; set; }
        public DateTime FirstAccessDate { get; set; }
        public DateTime LastAccessDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public CourseReferenceSchema Course { get; set; }
        public LearnerSchema Learner { get; set; }
        public ObjectiveSchema GlobalObjectives { get; set; }
        public ActivityResultSchema ActivityDetails { get; set; }
        public SharedDataEntrySchema SharedData { get; set; }
        public string SuspendedActivityId { get; set; }
        public double RegistrationCompletionAmount { get; set; }

    }

    public enum RegistrationCompletion
    {
        UNKNOWN,
        COMPLETED,
        INCOMPLETE
    }
    public enum RegistrationSuccess
    {
        UNKNOWN,
        PASSED,
        FAILED
    }
}
