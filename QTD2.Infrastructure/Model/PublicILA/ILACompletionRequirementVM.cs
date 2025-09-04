using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicILA
{
    public class ILACompletionRequirementVM
    {
        public bool IsPreTestRequired {  get; set; }
        public bool IsCBTRequired {  get; set; }
        public bool IsFinalTestRequired {  get; set; }
        public bool IsTaskQualificationRequired {  get; set; }
        public bool IsStudentEvaluationRequired {  get; set; }
        public bool IsSimulatorScenarioRequired { get; set; }
        public List<PublicClassAvailableSeatsVM> AvailableSeatsDetails { get; set; } = new List<PublicClassAvailableSeatsVM>();
        public ILACompletionRequirementVM()
        {
            
        }

        public ILACompletionRequirementVM(bool isPreTestRequired, bool isCBTRequired, bool isFinalTestRequired, bool isTaskQualificationRequired, bool isStudentEvaluationRequired, bool isSimulatorScenarioRequired)
        {
            IsPreTestRequired = isPreTestRequired;
            IsCBTRequired = isCBTRequired;
            IsFinalTestRequired = isFinalTestRequired;
            IsTaskQualificationRequired = isTaskQualificationRequired;
            IsStudentEvaluationRequired = isStudentEvaluationRequired;
            IsSimulatorScenarioRequired = isSimulatorScenarioRequired;
        }

    }

    public class PublicClassAvailableSeatsVM
    {
        public int ClassScheduleId { get; set; }
        public int? AvailableSeat { get; set; }
        public PublicClassAvailableSeatsVM()
        {
            
        }

        public PublicClassAvailableSeatsVM(int availableSeat, int classScheduleId)
        {
            ClassScheduleId = classScheduleId;
            AvailableSeat = availableSeat;
        }
    }
}
