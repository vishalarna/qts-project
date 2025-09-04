using QTD2.Infrastructure.Model.CBT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Employee
{
    public class ClassScheduleEmployee_OnlineCourseVM
    {
        public int ClassScheduleId { get; set; }
        public string ILANumber { get; set; }
        public string ILAName { get; set; }
        public DateTime? PlannedDate { get; set; }
        public bool? UsePreTestAndTest { get; set; }
        public bool? PreTestRequired { get; set; }
        public string ProviderName { get; set; }
        public string InstructorName { get; set; }
        public string LocationName { get; set; }
        public double? TotalTrainingHours { get; set; }
        public string CBTLearningContractInstructions { get; set; }
        public DateTime? CompletionDate { get; set; }
        public List<CBT_ScormRegistrationVM> ScormRegistrations { get; set; } = new List<CBT_ScormRegistrationVM>();

        public ClassScheduleEmployee_OnlineCourseVM() { }
        public ClassScheduleEmployee_OnlineCourseVM(int classScheduleId,string iLANumber,string iLAName,DateTime? plannedDate,bool? usePreTestAndTest, bool? preTestRequired, string providerName,string instructorName,string locationName,double? totalTrainingHours,string cBTLearningContractInstructions,DateTime? completionDate,List<CBT_ScormRegistrationVM> scormRegistrations = null) // Optional parameter for list
        {
            ClassScheduleId = classScheduleId;
            ILANumber = iLANumber;
            ILAName = iLAName;
            PlannedDate = plannedDate;
            UsePreTestAndTest = usePreTestAndTest;
            PreTestRequired = preTestRequired;
            ProviderName = providerName;
            InstructorName = instructorName;
            LocationName = locationName;
            TotalTrainingHours = totalTrainingHours;
            CBTLearningContractInstructions = cBTLearningContractInstructions;
            CompletionDate = completionDate;
            ScormRegistrations = scormRegistrations ?? new List<CBT_ScormRegistrationVM>();
        }
    }
}
