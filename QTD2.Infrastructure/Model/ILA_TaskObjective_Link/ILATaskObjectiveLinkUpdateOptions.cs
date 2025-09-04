using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_TaskObjective_Link
{
    public class ILATaskObjectiveLinkUpdateOptions
    { 
        public List<TaskSequenceModel> TaskLinks { get; set; }
        public bool IsIncludeProcedures { get; set; } = false;
        public bool IsIncludeEos { get; set; } = false;
        public bool IsIncludeMetaTask { get; set; } = false;
    }
}
