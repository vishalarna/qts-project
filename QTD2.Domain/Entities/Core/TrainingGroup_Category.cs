using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingGroup_Category : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Note { get; set; }

        public virtual ICollection<TrainingGroup> TrainingGroups { get; set; } = new List<TrainingGroup>();

        public TrainingGroup_Category()
        {
        }

        public TrainingGroup_Category(string title, string description, DateTime effectiveDate, string note)
        {
            Title = title;
            Description = description;
            EffectiveDate = effectiveDate;
            Note = note;
        }

        public TrainingGroup AddTrainingGroup(TrainingGroup group)
        {
            var training_group = TrainingGroups.FirstOrDefault(x => x.Id == group.Id && x.CategoryId == this.Id);
            if(training_group == null)
            {
                AddEntityToNavigationProperty<TrainingGroup>(group);
            }
            return group;
        }

        public void RemoveTrainingGroup(TrainingGroup group)
        {
            var training_group = TrainingGroups.FirstOrDefault(x => x.Id == group.Id && x.CategoryId == this.Id);
            if (training_group != null)
            {
                RemoveEntityFromNavigationProperty<TrainingGroup>(training_group);
            }
        }
    }
}
