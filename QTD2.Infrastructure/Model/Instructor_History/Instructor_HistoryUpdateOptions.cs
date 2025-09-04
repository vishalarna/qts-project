using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor_History
{
    public class Instructor_HistoryUpdateOptions
    {
        public int InstructorId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string InstructorNotes { get; set; }
    }
}
