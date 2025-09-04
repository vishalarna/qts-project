using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ClassSchedule
    {
        public ClassSchedule()
        {
            ClassScheduleEmployees = new HashSet<ClassScheduleEmployee>();
            InverseRecurrence = new HashSet<ClassSchedule>();
        }

        public int Id { get; set; }
        public int? RecurrenceId { get; set; }
        public bool? IsRecurring { get; set; }
        public int? ProviderId { get; set; }
        public int? Ilaid { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int? InstructorId { get; set; }
        public int? LocationId { get; set; }
        public string SpecialInstructions { get; set; }
        public string WebinarLink { get; set; }
        public string Notes { get; set; }
        public bool? Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Ila Ila { get; set; }
        public virtual ClassSchedule Recurrence { get; set; }
        public virtual ICollection<ClassScheduleEmployee> ClassScheduleEmployees { get; set; }
        public virtual ICollection<ClassSchedule> InverseRecurrence { get; set; }
    }
}
