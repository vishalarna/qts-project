using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleData
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public bool Active { get; set; } = true;
        public int? RecurrenceId { get; set; }

        public bool IsRecurring { get; set; }

        public int? ProviderID { get; set; }

        public int? ILAID { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? InstructorId { get; set; }

        public int? LocationId { get; set; }

        public string SpecialInstructions { get; set; }

        public string WebinarLink { get; set; }

        public string Notes { get; set; }

        public bool CanDelete { get; set; }

        public QTD2.Domain.Entities.Core.Location Location { get; set; }

        public QTD2.Domain.Entities.Core.Instructor Instructor { get; set; }
        public QTD2.Domain.Entities.Core.ILA ILA { get; set; }

        public ICollection<QTD2.Domain.Entities.Core.ClassSchedule_StudentEvaluations_Link> ClassSchedule_StudentEvaluations_Links { get; set; } = new List<QTD2.Domain.Entities.Core.ClassSchedule_StudentEvaluations_Link>();

        public int EmployeeCount { get; set; }

        public bool IsStartAndEndTimeEmpty { get; set; } = false;




    }
}
