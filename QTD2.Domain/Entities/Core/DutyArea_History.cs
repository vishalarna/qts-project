using System;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class DutyArea_History : Entity
    {
         public int DutyAreaId { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual DutyArea DutyArea { get; set; }

        public DutyArea_History(int dutyAreaId, DateTime changeEffectiveDate, string changeNotes)
        {
            DutyAreaId = dutyAreaId;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public DutyArea_History()
        {
        }
    }
}
