using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleReviewDataVM
    {
        public int TotalStudents { get; set; }
        public int EnrolledStudents { get; set; }
        public string SelectionsMadeFor { get; set; }
        public string NotificationsEnabledFor { get; set; }
    }
}
