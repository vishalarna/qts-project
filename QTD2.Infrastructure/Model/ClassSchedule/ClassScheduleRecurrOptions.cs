using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleRecurrOptions
    {
        public DateTime StartDate {  get; set; }

        public DateTime EndDate { get; set; }
        public bool IsPubliclyAvailable { get; set; }
    }
}
