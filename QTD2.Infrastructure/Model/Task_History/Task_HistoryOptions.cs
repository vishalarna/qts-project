using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_History
{
    public class Task_HistoryOptions
    {
        public int[] TaskIds { get; set; }

        public string ChangeNotes { get; set; }

        public int Version_TaskId { get; set; }

        public DateTime EffectiveDate { get; set; }
        public bool OldStatus { get; set; }
        public bool NewStatus { get; set; }

        public Task_HistoryOptions()
        {

        }
        public Task_HistoryOptions(DateTime effectiveDate,int[] taskIds,string changeNotes,int versionTaskId)
        {
            EffectiveDate = effectiveDate;
            TaskIds = taskIds;
            ChangeNotes = changeNotes;
            Version_TaskId = versionTaskId;
        }
    }
}
