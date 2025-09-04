using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_StudentEvaluations_Link : Entity
    {
        public int StudentEvaluationId { get; set; }

        public int ClassScheduleId { get; set; }

        public virtual StudentEvaluation StudentEvaluation { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        public ClassSchedule_StudentEvaluations_Link()
        {
        }

        public ClassSchedule_StudentEvaluations_Link(StudentEvaluation studentEvaluation, ClassSchedule classSchedule)
        {
            StudentEvaluation = studentEvaluation;
            ClassSchedule = classSchedule;
            this.StudentEvaluationId = studentEvaluation.Id;
            this.ClassScheduleId = classSchedule.Id;
        }

        public void UpdateClassEval(int studentEvalId)
        {
            StudentEvaluationId = studentEvalId;
        }
    }
}
