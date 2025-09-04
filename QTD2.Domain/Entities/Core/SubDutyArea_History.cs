using System;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SubDutyArea_History : Entity
    {
        public int SubDutyAreaId { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual SubdutyArea SubDutyArea { get; set; }

        public SubDutyArea_History(int subDutyAreaId, DateTime changeEffectiveDate, string changeNotes)
        {
            SubDutyAreaId = subDutyAreaId;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public SubDutyArea_History()
        {
        }
    }
}
