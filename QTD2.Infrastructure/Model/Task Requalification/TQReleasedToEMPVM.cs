using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQReleasedToEMPVM
    {
        public string EmpFName { get; set; }

        public string EmpLName { get; set; }

        public string EmpName { get; set; }

        public string EmpCommaSepName { get; set; }

        public int EmpId { get; set; }

        public string TaskNumber { get; set; }

        public string Number { get; set; }

        public string TaskDescription { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string EvaluatorName { get; set; }

        public string RequiredTaskQuals { get; set; }

        public List<string> EvaluatorIds { get; set; } = new List<string>();

        public int Id { get; set; }

        public bool WasTQStarted { get; set; }
    }
}
