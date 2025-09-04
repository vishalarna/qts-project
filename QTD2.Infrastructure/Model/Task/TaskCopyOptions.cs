using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskCopyOptions
    {
        public int SubDutyAreaId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public bool IsReliability { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public List<int> Positions { get; set; }
    }
}
