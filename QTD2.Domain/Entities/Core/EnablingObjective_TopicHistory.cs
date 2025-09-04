using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_TopicHistory : Entity
    {
        public int EnablingObjectiveTopicId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual EnablingObjective_Topic EnablingObjective_Topic { get; set; }

        public EnablingObjective_TopicHistory()
        {
        }

        public EnablingObjective_TopicHistory(int enablingObjectiveTopicId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            EnablingObjectiveTopicId = enablingObjectiveTopicId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }
    }
}
