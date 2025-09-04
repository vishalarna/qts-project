using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskReQualificationTabVM
    {
        public int TaskId { get; set; }

        public int EmpLinkCount { get; set; }

        public string TaskNumber { get; set; }

        public string TaskDescription { get; set; }

        public bool RequalificationRequired { get; set; }

        public string Position { get; set; }

        public string Number { get; set; }

        public DateOnly? DueDate { get; set; }
    }
}
