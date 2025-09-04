using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA.EvaluationReleaseSetting
{
    public class EvaluationReleaseEMPSettingCreateOptions
    {
        public int ILAId { get; set; }

        public bool EvaluationRequired { get; set; }

        public bool EvaluationUsedToDeployStudentEvaluation { get; set; }
        
        public bool EvaluationAvailableOnStartDate { get; set; }

        public bool EvaluationAvailableOnEndDate { get; set; }

        public bool FinalGradeRequired { get; set; }

        public bool ReleaseOnSpecificTimeAfterClassEndDate { get; set; }

        public int? ReleaseAfterEndTime { get; set; }

        public bool ReleasePrior { get; set; }

        public bool ReleaseAfterGradeAssigned { get; set; }

        public int? EvaluationDueDate { get; set; }
        public int EmpSettingsReleaseTypeId { get; set; }
    }
}
