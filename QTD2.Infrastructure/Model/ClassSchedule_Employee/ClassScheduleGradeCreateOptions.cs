using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Employee
{
    public class ClassScheduleGradeCreateOptions
    {
        public int Score { get; set; }
        public string Grade { get; set; }
        public string? BulkGrade { get; set; }
        public DateTime? BulkCompDate { get; set; }
        public string GradeNotes { get; set; }
        public DateTime? completionDate { get; set; }

        public int? ClassId { get; set; }
        public int? bulkScore { get; set; }
        public string? bulkGradeNote { get; set; }
        public bool? IsQualificationCompleted { get; set; }
    }
}
