using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_StudentEvaluation_Link
{
    public class ClassScheduleWithCountOptions
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }

        public string EvaluationName { get; set; }
        public ClassScheduleWithCountOptions()
        {

        }

        public ClassScheduleWithCountOptions(int id, DateTime startDateTime, string evaluationName)
        {
            Id = id;
            StartDateTime = startDateTime;
            EvaluationName = evaluationName;
        }
    }
}
