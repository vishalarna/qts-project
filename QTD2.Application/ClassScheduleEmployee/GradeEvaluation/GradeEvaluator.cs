using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Helpers.Implementations.ClassScheduleEmployee.GradeEvaluation
{
    public class GradeEvaluator
    {
        public GradeEvaluationResult EvaluateClassScheduleEmployee(
                            ClassSchedule_Employee classSchedule_Employee,
                            ClassSchedule classSchedule,
                            List<ClassSchedule_Roster> tests,
                            CBT_ScormRegistration cbt_scormRegistration
                            )
        {


            return new GradeEvaluationResult();
        }
    }
}
