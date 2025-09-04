using System;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskCreateOptions
    {
        public int SubDutyAreaId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public string Criteria { get; set; }

        public string Image { get; set; }

        public string Abbreviation { get; set; }

        public string TaskCriteriaUpload { get; set; }

        public string Standards { get; set; }

        public bool Critical { get; set; }

        public string Tools { get; set; }

        public string References { get; set; }

        public int RequiredTime { get; set; }

        public bool IsSignificant { get; set; }

        public bool IsMeta { get; set; }

        public bool IsReliability { get; set; }

        public string Conditions { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
