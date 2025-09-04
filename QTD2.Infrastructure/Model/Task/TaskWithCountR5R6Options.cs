using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.PositionTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskWithCountR5R6Options
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public int TrainingGroupLinkCount { get; set; }

        public bool Active { get; set; }

        public int DANumber { get; set; }

        public int SDANumber { get; set; }

        public string Letter { get; set; }

        public bool IsUsedForTQ { get; set; }

        public int TaskNumber { get; set; }

        public string OrderProperty { get; set; }

        public bool? IsRR { get; set; }

        public bool IsR6Impacted { get; set; }

        public string R6ImpactedReason { get; set; }

        public DateTime? R6ImpactedEffectiveDate { get; set; }

        public bool IsR5Impacted { get; set; }

        public int PositionTaskId { get; set; }

        public List<R5ImpactedTaskResponse> R5ImpactedTasks { get; set; }

        public TaskWithCountR5R6Options()
        {
        }

        public TaskWithCountR5R6Options(string number, string description, int id, int linkCount, bool active, int trainingGroupLinkCount,bool isR6Impacted, string r6ImpactedReason, DateTime? r6ImpactedEffectiveDate, bool isR5Impacted,int positionTaskId,List<R5ImpactedTaskResponse> r5ImpactedTasks, bool isRR = false)
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
            TrainingGroupLinkCount = trainingGroupLinkCount;
            IsRR = isRR;
            IsR6Impacted = isR6Impacted;
            R6ImpactedReason = r6ImpactedReason;
            R6ImpactedEffectiveDate = r6ImpactedEffectiveDate;
            IsR5Impacted = isR5Impacted;
            PositionTaskId = positionTaskId;
            R5ImpactedTasks = r5ImpactedTasks;
        }
    }
}
