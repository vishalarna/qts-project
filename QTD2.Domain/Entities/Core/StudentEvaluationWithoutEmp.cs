using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluationWithoutEmp : Entity
    {
        public int StudentEvaluationId { get; set; }

        public int? EmployeeId { get; set; }
        public int ClassScheduleId { get; set; }
        public int QuestionId { get; set; }

        public string DataMode { get; set; } 

        public Nullable<int> RatingScale { get; set; }

        public double High { get; set; }
        public double Average { get; set; }
        public double Low { get; set; }

        public string Notes { get; set; }

        public string AdditionalComments { get; set; }

        public Nullable<bool> IsCompleted { get; set; }

        public virtual StudentEvaluation StudentEvaluation { get; set; }

        public virtual RatingScaleExpanded RatingScaleExpanded { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        //public virtual StudentEvaluationQuestion StudentEvaluationQuestion { get; set; }

        public virtual QuestionBank QuestionBank { get; set; }

        public virtual Employee Employee { get; set; }

        public StudentEvaluationWithoutEmp()
        {
        }

        public StudentEvaluationWithoutEmp(int studentEvaluationId, int classScheduleId, int questionId, string dataMode, int? ratingScale, double high, double average, double low, string notes, string additionalComments, bool? isCompleted, int? employeeId)
        {
            StudentEvaluationId = studentEvaluationId;
            ClassScheduleId = classScheduleId;
            QuestionId = questionId;
            DataMode = dataMode;
            RatingScale = ratingScale;
            High = high;
            Average = average;
            Low = low;
            Notes = notes;
            AdditionalComments = additionalComments;
            IsCompleted = isCompleted;
            EmployeeId = employeeId;
        }
    }
}
