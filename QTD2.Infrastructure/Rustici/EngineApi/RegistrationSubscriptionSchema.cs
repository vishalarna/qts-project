using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class RegistrationSubscriptionSchema
    {
        public string Id { get; set; }
        public int Instance { get; set; }
        public string XapiRegistrationId { get; set; }
        public string Lti13SubjectIdentifier { get; set; }
        public string Lti13CustomClaims { get; set; }
        public string DispatchId { get; set; }
        public string Updated { get; set; }
        public RegistrationCompletion RegistrationCompletion { get; set; }
        public RegistrationSuccess RegistrationSuccess { get; set; }
        public ScoreSchema Score { get; set; }
        public double TotalSecondsTracked { get; set; }
        public string FirstAccessDate { get; set; }
        public string LastAccessDate { get; set; }
        public string CompletedDate { get; set; }
        public string CreatedDate { get; set; }
        public CourseSchema Course { get; set; }
        public LearnerSchema Learner { get; set; }
        public List<ObjectiveSchema> GlobalObjectives { get; set; }
        public ActivityResultSchema ActivityDetails { get; set; }
        public RuntimeSchema Runtime { get; set; }
        public List<SharedDataSchema> SharedData { get; set; }
        public string SuspendedActivityId { get; set; }
        public double RegistrationCompletionAmount { get; set; }
        public string Description { get; set; }
    }

    public enum TypeEnum { Boolean, Integer, Object, String };

    public partial class Resources
    {
        public Course Course { get; set; }
        public Registration Registration { get; set; }
    }

    public partial class Course
    {
        public string Id { get; set; }
        public string LearningStandard { get; set; }
        public string Version { get; set; }
    }

    public partial class Registration
    {
        public string Id { get; set; }
        public string Instance { get; set; }
        public Learner Learner { get; set; }
        public string IsDispatch { get; set; }
    }

    public partial class Learner
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CourseSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
    }

    public class SharedDataSchema
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }


}
