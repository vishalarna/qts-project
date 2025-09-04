using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TQILAEmpSetting : Common.Entity
    {
        public TQILAEmpSetting()
        {    
        }

        public TQILAEmpSetting(int iLAId, bool tQRequired, bool releaseAtOnce, bool releaseOneAtTime, bool releaseOnClassStart, bool releaseOnClassEnd, int? specificTime, bool priorToSpecificTime, bool oneSignOffRequired, int tQDueDate, int? multipleSignOffRequired, int empSettingsReleaseTypeId, bool showTaskSuggestions, bool showTaskQuestions)
        {
            ILAId = iLAId;
            TQRequired = tQRequired;
            ReleaseAtOnce = releaseAtOnce;
            ReleaseOneAtTime = releaseOneAtTime;
            ReleaseOnClassStart = releaseOnClassStart;
            ReleaseOnClassEnd = releaseOnClassEnd;
            SpecificTime = specificTime;
            PriorToSpecificTime = priorToSpecificTime;
            OneSignOffRequired = oneSignOffRequired;
            MultipleSignOffRequired = multipleSignOffRequired;
            TQDueDate = tQDueDate;
            EmpSettingsReleaseTypeId = empSettingsReleaseTypeId;
            ShowTaskSuggestions = showTaskSuggestions;
            ShowTaskQuestions = showTaskQuestions;
        }

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

        public int? EmpSettingsReleaseTypeId { get; set; }
        public bool ShowTaskSuggestions { get; set; }
        public bool ShowTaskQuestions { get; set; }
        public virtual ILA ILA { get; set; }
        public virtual EmpSettingsReleaseType EmpSettingsReleaseType { get; set; }

        public DateTime GetDueDate(DateTime dueDate)
        {
            if (EmpSettingsReleaseTypeId == 3)
            {
                return dueDate.AddMonths(TQDueDate);
            }
            else if (EmpSettingsReleaseTypeId == 2)
            {
                return dueDate.AddDays(TQDueDate * 7);
            }
            else if (EmpSettingsReleaseTypeId == 1)
            {
                return dueDate.AddDays(TQDueDate);
            }
            return DateTime.MaxValue;
        }

    }
}
