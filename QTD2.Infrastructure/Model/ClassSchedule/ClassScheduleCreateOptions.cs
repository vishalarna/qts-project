using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleCreateOptions
    {
        public int? ProviderID { get; set; }

        public int? ILAID { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
        public int? InstructorId { get; set; }

        public int? LocationId { get; set; }

        public string SpecialInstructions { get; set; }

        public string WebinarLink { get; set; }
        public string Notes { get; set; }
        public int ClassSize { get; set; }

        public bool IsStartAndEndTimeEmpty { get; set; }
        public bool IsPubliclyAvailable { get; set; }
        public List<ClassScheduleRecurrOptions> RecurringOptions { get; set; } = new List<ClassScheduleRecurrOptions>();
    }
}
