using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleVM
    {
        public int Id { get; set; }
        public int? RecurrenceId { get; set; }

        public bool IsRecurring { get; set; }
        public int? ProviderID { get; set; }

        public string Provider { get; set; }

        public int? ILAID { get; set; }

        public string ILA { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string Instructor { get; set; }

        public string Location { get; set; }

        public int? InstructorId { get; set; }

        public int? LocationId { get; set; }
        //public List<QTD2.Domain.Entities.Core.ILA_Topic_Link> TopicLinks { get; set; }
        public string SpecialInstructions { get; set; }

        public string WebinarLink { get; set; }

        public string Notes { get; set; }

        public bool CanDelete { get; set; }
        public IEnumerable<int> TopicIds { get; set; }
    }
}
