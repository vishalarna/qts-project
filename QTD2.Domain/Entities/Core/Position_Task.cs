using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Domain.Entities.Core
{
    public class Position_Task : Common.Entity
    {
        public Position_Task(Position position, Task task)
        {
            PositionId = position.Id;
            TaskId = task.Id;
            Position = position;
            Task = task;
        }
        public Position_Task()
        {
        }
        public int PositionId { get; set; }

        public int TaskId { get; set; }
        public virtual Position Position { get; set; }
        public virtual Task Task { get; set; }
		public bool IsR6Impacted { get; set; }
        public string R6ImpactedReason { get; set; }
        public DateTime? R6ImpactedEffectiveDate { get; set; }
		public bool IsR5Impacted
        {
            get
            {
                return R5ImpactedTasks.Where(r=>!r.Deleted).Count() > 0;
            }
        }

        public virtual ICollection<R5ImpactedTask> R5ImpactedTasks { get; set; } = new List<R5ImpactedTask>();

        public void SetIsR6Impacted(bool isR6Impacted, string r6ImpactedReason, DateTime? r6ImpactedEffectiveDate)
        {
            IsR6Impacted = isR6Impacted;
            R6ImpactedReason = r6ImpactedReason;
            R6ImpactedEffectiveDate = r6ImpactedEffectiveDate;
        }

        public List<int> RemoveAllR5ImpactedTasks()
        {
            List<R5ImpactedTask> r5ImpactedTasksToDelete = R5ImpactedTasks.Where(r => !r.Deleted).ToList();
            foreach (R5ImpactedTask r5ImpactedTask in r5ImpactedTasksToDelete)
            {
                r5ImpactedTask.Delete();
            }

            return r5ImpactedTasksToDelete.Select(r=>r.ImpactedTaskId).ToList();
        }

        public (List<int> removedTaskIds, List<int> addedTaskIds) SetR5ImpactedTasks(List<int> taskIdsToLink)
        {
			List<R5ImpactedTask> oldR5ImpactedTasks = R5ImpactedTasks.Where(r => !r.Deleted && !taskIdsToLink.Contains(r.ImpactedTaskId)).ToList();

            //Soft-delete old R5ImpactedTasks that aren't in taskIdsToLink
            foreach (R5ImpactedTask oldR5ImpactedTask in oldR5ImpactedTasks)
            {
                oldR5ImpactedTask.Delete();
            }

            List<int> existingImpactedTaskIds = R5ImpactedTasks.Where(r => !r.Deleted).Select(r => r.ImpactedTaskId).ToList();
            List<int> newR5ImpactedTaskIds = taskIdsToLink.Where(t => !existingImpactedTaskIds.Contains(t)).ToList();

            //Add new R5ImpactedTasks
            foreach (int newR5ImpactedTaskId in newR5ImpactedTaskIds)
            {
                R5ImpactedTasks.Add(new R5ImpactedTask(Id, newR5ImpactedTaskId));
            }

            return (oldR5ImpactedTasks.Select(o => o.ImpactedTaskId).ToList(), newR5ImpactedTaskIds);
        }

        public int DeleteR5Task(int r5ImpactedTaskId)
        {
            R5ImpactedTask r5ImpactedTask = R5ImpactedTasks.Single(r => !r.Deleted && r.Id == r5ImpactedTaskId);
            r5ImpactedTask.Delete();
            return r5ImpactedTask.ImpactedTaskId;
        }
    }
}
