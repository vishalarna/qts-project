using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TQEmpSetting : Common.Entity
    {

        public int TaskQualificationId { get; set; }

        public bool ReleaseToAllSingleSignOff { get; set; }

        public int? MultipleSignOff { get; set; }

        public bool ReleaseOnReleaseDate { get; set; }

        public bool ReleaseInSpecificOrder { get; set; }

        public bool ShowTaskSuggestions { get; set; }

        public bool ShowTaskQuestions { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public virtual TaskQualification TaskQualification { get; set; }

        public int? MultipleSignOffDisplay
        {
            get
            {
                return MultipleSignOff;
            }
        }

        public TQEmpSetting(int taskQualificationId,
            int? multipleSignOff,
            bool releaseToAllSingleSignOff,
            bool releaseOnReleaseDate,
            bool releaseInSpecificOrder,
            bool showTaskSuggestions,
            bool showTaskQuestions
            )
        {
            TaskQualificationId = taskQualificationId;
            MultipleSignOff = multipleSignOff;
            ReleaseToAllSingleSignOff = releaseToAllSingleSignOff;
            ReleaseOnReleaseDate = releaseOnReleaseDate;
            ReleaseInSpecificOrder = releaseInSpecificOrder;
            ShowTaskSuggestions = showTaskSuggestions;
            ShowTaskQuestions = showTaskQuestions;
        }

        public TQEmpSetting()
        {
        }
    }
}
