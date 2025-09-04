using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class SaftyHazard_Category : Common.Entity
    {
        public string Description { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public string? Notes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public virtual ICollection<SaftyHazard> SaftyHazards { get; set; } = new List<SaftyHazard>();

        public virtual ICollection<SafetyHazard_CategoryHistory> SafetyHazard_CategoryHistories { get; set; } = new List<SafetyHazard_CategoryHistory>();

        public SaftyHazard_Category(string description, int number, string title, string notes, DateTime? effectiveDate)
        {
            Description = description;
            Number = number;
            Title = title;
            Notes = notes;
            EffectiveDate = effectiveDate;
        }

        public SaftyHazard_Category()
        {
        }

        public SaftyHazard AddSaftyHazard(SaftyHazard saftyHazard)
        {
            var sh = SaftyHazards.FirstOrDefault(x => x.Title.ToLower() == saftyHazard.Title.ToLower());
            if (sh == null)
            {
                AddEntityToNavigationProperty<SaftyHazard>(saftyHazard);
            }

            return saftyHazard;
        }

        public void RemoveSaftyHazard(SaftyHazard saftyHazard)
        {
            var sh = SaftyHazards.FirstOrDefault(x => x.Title.ToLower() == saftyHazard.Title.ToLower());
            if (sh != null)
            {
                RemoveEntityFromNavigationProperty<SaftyHazard>(saftyHazard);
            }
        }
    }
}
