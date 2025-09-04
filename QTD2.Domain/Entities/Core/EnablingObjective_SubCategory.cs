using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_SubCategory : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int? Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        public virtual EnablingObjective_Category EnablingObjectives_Category { get; set; }

        public virtual ICollection<EnablingObjective_Topic> EnablingObjective_Topics { get; set; } = new List<EnablingObjective_Topic>();

        public virtual ICollection<EnablingObjective> EnablingObjectives { get; set; } = new List<EnablingObjective>();

        public virtual ICollection<CustomEnablingObjective> CustomEnablingObjectives { get; set; } = new List<CustomEnablingObjective>();

        public virtual ICollection<EnablingObjective_SubCategoryHistory> EnablingObjective_SubCategoryHistories { get; set; } = new List<EnablingObjective_SubCategoryHistory>();

        
        public EnablingObjective_SubCategory(string description, int categoryId, int number, string title, DateTime effectiveDate)
        {
            Description = description;
            CategoryId = categoryId;
            Number = number;
            Title = title;
            EffectiveDate = effectiveDate;
        }

        public EnablingObjective_SubCategory()
        {
        }

        public EnablingObjective_Topic AddTopic(EnablingObjective_Topic enablingObjectives_Topic)
        {
            AddEntityToNavigationProperty<EnablingObjective_Topic>(enablingObjectives_Topic);
            return enablingObjectives_Topic;
        }

        public void CreateHistory(bool oldStatus, bool newStatus, DateTime effectiveDate, string reason)
        {
            EnablingObjective_SubCategoryHistory history = new EnablingObjective_SubCategoryHistory()
            {
                Active = true,
                ChangeEffectiveDate = effectiveDate,
                ChangeNotes = reason,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                Deleted = false,
                NewStatus = newStatus,
                OldStatus = oldStatus
            };

            AddEntityToNavigationProperty<EnablingObjective_SubCategoryHistory>(history);
        }

        //public EnablingObjective AddEnablingObective(EnablingObjective enablingObjective)
        //{
        //    AddEntityToNavigationProperty<EnablingObjective>(enablingObjective);
        //    return enablingObjective;
        //}
    }
}
