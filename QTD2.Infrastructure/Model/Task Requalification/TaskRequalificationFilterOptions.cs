using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskRequalificationFilterOptions
    {
        public int? PosId { get; set; }

        public int? DaId { get; set; }

        public int? SdaId { get; set; }

        public int? TaskId { get; set; }

        public int? SqCatId { get; set; }

        public int? SqSubCatId { get; set; }

        public int? SqTopicId { get; set; }

        public int? SqId { get; set; }

        public int? TaskTGId { get; set; }

        public int? EmpId { get; set; }

        public int? EvalId { get; set; }

        public bool RrtOnly { get; set; }

        public bool MetaOnly { get; set; }

        public bool SpecificDateRange { get; set; }

        public string FilterBy { get; set; }
    }
}
