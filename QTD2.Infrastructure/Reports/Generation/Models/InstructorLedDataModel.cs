using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using System;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class InstructorLedDataModel 
    {
       
        public ClassSchedule ClassSchedule { get; set; }
        public StudentEvaluation StudentEvaluation { get; set; }
        public List<ClassSchedule_Evaluation_Roster> ClassScheduleEvaluationRosters { get; set; }
        public List<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; }

        public InstructorLedDataModel(ClassSchedule classSchedule, StudentEvaluation studentEvaluation, List<ClassSchedule_Evaluation_Roster> classScheduleEvaluationRosters, List<StudentEvaluationWithoutEmp> studentEvaluationWithoutEmps)
        {
            ClassSchedule = classSchedule;
            StudentEvaluation = studentEvaluation;
            ClassScheduleEvaluationRosters = classScheduleEvaluationRosters;
            StudentEvaluationWithoutEmps = studentEvaluationWithoutEmps;
        }
    }
}
