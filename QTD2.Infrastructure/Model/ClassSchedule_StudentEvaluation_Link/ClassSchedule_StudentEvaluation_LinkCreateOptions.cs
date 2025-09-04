using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_StudentEvaluation_Link
{
    public class ClassSchedule_StudentEvaluation_LinkCreateOptions
    {
        public string selection { get; set; }
        public int ILAId { get; set; }
        public List<int> classScheduleIds { get; set; }
    }

    public class ClassSchedule_StudentEvaluation_LinkUpdateOptions
    {
        public int ilaId { get; set; }
        public int evalId { get; set; }
        public int audienceId { get; set; }
    }
}
