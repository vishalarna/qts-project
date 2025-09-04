using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Instructor_History : Entity
    {
        public Instructor_History(int instructorId, string instructornotes, DateTime effectivedate)
        {
            InstructorId = instructorId;
            InstructorNotes = instructornotes;
            EffectiveDate = effectivedate;
        }

        public Instructor_History()
        {
        }

        public int InstructorId { get; set; }

        public string InstructorNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
