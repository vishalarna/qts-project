using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class SubdutyArea : Common.Entity
    {
        public int DutyAreaId { get; set; }

        public string Description { get; set; }

        public int SubNumber { get; set; }

        public string Title { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ReasonForRevision { get; set; }

        public virtual DutyArea DutyArea { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

        public virtual ICollection<SubDutyArea_History> SubDutyArea_Histories { get; set; } = new List<SubDutyArea_History>();

        public SubdutyArea(int dutyAreaId, string description, int subNumber, string title, DateTime effectiveDate, string reasonForRevision)
        {
            DutyAreaId = dutyAreaId;
            Description = description;
            SubNumber = subNumber;
            Title = title;
            EffectiveDate = effectiveDate;
            ReasonForRevision = reasonForRevision;
        }

        public SubdutyArea()
        {
        }

        public Task AddTask(Task task)
        {
            var taskToAdd = Tasks.FirstOrDefault(x => x.Description.ToLower() == task.Description.ToLower());
            if (taskToAdd == null)
            {
                AddEntityToNavigationProperty<Task>(task);
            }

            return task;
        }

        public void RemoveTask(Task task)
        {
            var tasktoRemove = Tasks.FirstOrDefault(x => x.Id == task.Id);
            if (tasktoRemove != null)
            {
                RemoveEntityFromNavigationProperty<Task>(task);
            }
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnSubDutyAreaDeleted(this));
        }
    }
}
