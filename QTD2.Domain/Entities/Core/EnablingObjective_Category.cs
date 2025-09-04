using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Category : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        public virtual ICollection<EnablingObjective_SubCategory> EnablingObjective_SubCategories { get; set; } = new List<EnablingObjective_SubCategory>();

        public virtual ICollection<EnablingObjective_CategoryHistory> EnablingObjective_CategoryHistories { get; set; } = new List<EnablingObjective_CategoryHistory>();

        public virtual ICollection<EnablingObjective> EnablingObjectives { get; set; } = new List<EnablingObjective>();
        public virtual ICollection<CustomEnablingObjective> CustomEnablingObjectives { get; set; } = new List<CustomEnablingObjective>();

        public EnablingObjective_Category(string description, int number, string title, DateTime effectiveDate)
        {
            Description = description;
            Number = number;
            Title = title;
            EffectiveDate = effectiveDate;
        }

        public EnablingObjective_Category()
        {
        }

        public EnablingObjective_SubCategory AddSubCategory(EnablingObjective_SubCategory enablingObjectives_SubCategory)
        {
            AddEntityToNavigationProperty<EnablingObjective_SubCategory>(enablingObjectives_SubCategory);
            return enablingObjectives_SubCategory;
        }
    }
}
