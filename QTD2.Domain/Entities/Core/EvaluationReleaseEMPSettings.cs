using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EvaluationReleaseEMPSettings : Common.Entity
    {
        public int ILAId { get; set; }

        public bool EvaluationRequired { get; set; }

        public bool EvaluationUsedToDeployStudentEvaluation { get; set; }
        public string jobDetails { get; set; }

        public bool EvaluationAvailableOnStartDate { get; set; }

        public bool EvaluationAvailableOnEndDate { get;set; }

        public bool FinalGradeRequired { get; set; }

        public bool ReleaseOnSpecificTimeAfterClassEndDate { get; set; }

        public int? ReleaseAfterEndTime { get; set; }

        public bool ReleasePrior { get; set; }

        public bool ReleaseAfterGradeAssigned { get; set; }

        public int? EvaluationDueDate { get; set; }
        public int? EmpSettingsReleaseTypeId { get; set; }
        public virtual ILA ILA { get; set; }
        public virtual EmpSettingsReleaseType EmpSettingsReleaseType { get; set; }
        public EvaluationReleaseEMPSettings(int iLAId,bool evaluationUsedToDeployStudentEvaluation, bool evaluationRequired, bool evaluationAvailableOnStartDate, bool evaluationAvailableOnEndDate, bool finalGradeRequired, bool releaseOnSpecificTimeAfterClassEndDate, int? releaseAfterEndTime, bool releasePrior, bool releaseAfterGradeAssigned, int? evaluationDueDate, int empSettingsReleaseTypeId)
        {
            ILAId = iLAId;
            EvaluationRequired = evaluationRequired;
            EvaluationAvailableOnStartDate = evaluationAvailableOnStartDate;
            EvaluationAvailableOnEndDate = evaluationAvailableOnEndDate;
            FinalGradeRequired = finalGradeRequired;
            ReleaseOnSpecificTimeAfterClassEndDate = releaseOnSpecificTimeAfterClassEndDate;
            ReleaseAfterEndTime = releaseAfterEndTime;
            ReleasePrior = releasePrior;
            ReleaseAfterGradeAssigned = releaseAfterGradeAssigned;
            EvaluationDueDate = evaluationDueDate;
            EvaluationUsedToDeployStudentEvaluation = evaluationUsedToDeployStudentEvaluation;
            EmpSettingsReleaseTypeId = empSettingsReleaseTypeId;
        }

        public EvaluationReleaseEMPSettings()
        {
        }
        public DateTime GetDueDate(DateTime dueDate)
        {
            if (EmpSettingsReleaseTypeId == 3)
            {
                return dueDate.AddMonths(EvaluationDueDate.GetValueOrDefault());
            }
            else if (EmpSettingsReleaseTypeId == 2)
            {
                return dueDate.AddDays(EvaluationDueDate.GetValueOrDefault() * 7);
            }
            else if (EmpSettingsReleaseTypeId == 1)
            {
                return dueDate.AddDays(EvaluationDueDate.GetValueOrDefault());
            }
            return DateTime.MaxValue;
        }
    }
}
