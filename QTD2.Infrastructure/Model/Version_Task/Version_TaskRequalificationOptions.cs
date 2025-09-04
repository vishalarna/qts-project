using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_Task
{
    public class Version_TaskRequalificationInfo
    {
        public DateOnly? RequalificationDueDate { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public bool RequalificationRequired { get; set; }
        public string RequalificationNotes { get; set; }

        public int? VersionId { get; set; }
    }
}
