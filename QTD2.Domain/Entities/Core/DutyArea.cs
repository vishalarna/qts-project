using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class DutyArea : Entities.Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Letter { get; set; }

        public int Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ReasonForRevision { get; set; }

        public virtual ICollection<SubdutyArea> SubdutyAreas { get; set; } = new List<SubdutyArea>();

        public virtual ICollection<DutyArea_History> DutyArea_Histories { get; set; } = new List<DutyArea_History>();

        public DutyArea(string title, string description, string letter, int number, DateTime effectiveDate, string reasonForRevision)
        {
            Title = title;
            Description = description;
            Letter = letter;
            Number = number;
            EffectiveDate = effectiveDate;
            ReasonForRevision = reasonForRevision;
        }

        public DutyArea()
        {
        }

        public SubdutyArea AddSubduty(SubdutyArea subdutyArea)
        {
            var sda = SubdutyAreas.FirstOrDefault(x => x.Description.ToLower() == subdutyArea.Description.ToLower());
            if (sda == null)
            {
                AddEntityToNavigationProperty<SubdutyArea>(subdutyArea);
            }

            return subdutyArea;
        }

        public void RemoveSubduty(SubdutyArea subdutyArea)
        {
            var sda = SubdutyAreas.FirstOrDefault(x => x.Description.ToLower() == subdutyArea.Description.ToLower());
            if (sda != null)
            {
                RemoveEntityFromNavigationProperty<SubdutyArea>(subdutyArea);
            }
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnDutyAreaDeleted(this));
        }
    }
}
