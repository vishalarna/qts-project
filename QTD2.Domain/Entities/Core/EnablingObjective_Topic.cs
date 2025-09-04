using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Topic : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SubCategoryId { get; set; }

        public int? Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        public virtual EnablingObjective_SubCategory EnablingObjectives_SubCategory { get; set; }

        public virtual ICollection<EnablingObjective> EnablingObjectives { get; set; } = new List<EnablingObjective>();

        public virtual ICollection<CustomEnablingObjective> CustomEnablingObjectives { get; set; } = new List<CustomEnablingObjective>();

        public virtual ICollection<EnablingObjective_TopicHistory> EnablingObjective_TopicHistories { get; set; } = new List<EnablingObjective_TopicHistory>();
        public EnablingObjective_Topic(string description, int subCategoryId, int number, string title, DateTime effectiveDate)
        {
            Description = description;
            SubCategoryId = subCategoryId;
            Number = number;
            Title = title;
            EffectiveDate = effectiveDate;
        }

        public EnablingObjective_Topic()
        {
        }

        public EnablingObjective AddEnablingObective(EnablingObjective enablingObjective)
        {
            AddEntityToNavigationProperty<EnablingObjective>(enablingObjective);
            return enablingObjective;
        }
    }
}
