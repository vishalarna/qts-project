using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TQEvaluatorILAEmpSettings
{
    public class TQEvaluatorILAEmpSettings
    {
        public int ILAId { get; set; }

        public bool TQRequired { get; set; }

        public bool ReleaseAtOnce { get; set; }

        public bool ReleaseOneAtTime { get; set; }

        public bool ReleaseOnClassStart { get; set; }

        public bool ReleaseOnClassEnd { get; set; }

        public int? SpecificTime { get; set; }

        public bool PriorToSpecificTime { get; set; }

        public bool OneSignOffRequired { get; set; }

        public int? MultipleSignOffRequired { get; set; }

        public int TQDueDate { get; set; }
        public int EmpSettingsReleaseTypeId { get; set; }
        public bool ShowTaskSuggestions { get; set; }
        public bool ShowTaskQuestions { get; set; }
    }
}
