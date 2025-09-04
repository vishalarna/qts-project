using System;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskUpdateOptions
    {
        public int SubdutyAreaId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public string Criteria { get; set; }

        public string Standards { get; set; }

        public bool Critical { get; set; }

        public string Tools { get; set; }

        public string References { get; set; }

        public int RequiredTime { get; set; }

        public bool isSignificant { get; set; }
        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }


        public bool IsMeta { get; set; }

        public bool IsReliability { get; set; }
    }
}
