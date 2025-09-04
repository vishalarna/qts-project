using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IDP
{
    public class UpdateGradeOptions
    {
        public string? Grade { get; set; }
        public string? Score { get; set; }
        public string? reason { get; set; }
        public bool? isCompleted { get; set; }
        public int ClassScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? completionDate { get; set; }
    }
}
