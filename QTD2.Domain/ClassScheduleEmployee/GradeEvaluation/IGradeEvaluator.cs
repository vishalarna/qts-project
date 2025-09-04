using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.ClassScheduleEmployee.GradeEvaluation
{
    public interface IGradeEvaluator
    {
        public System.Threading.Tasks.Task<GradeEvaluationResult> EvaluateClassScheduleEmployeeAsync(ClassSchedule_Employee classSchedule_Employee);
        public GradeEvaluationResult EvaluateClassScheduleEmployee(ClassSchedule_Employee classScheduleEmployee, ClassSchedule classSchedule, ILA ila, List<ClassSchedule_Roster> tests, CBT_ScormRegistration cbtScormRegistration, ClassSchedule_TestReleaseEMPSetting testSettings);        
    }
}
