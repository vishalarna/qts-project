using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class ReleasedTQAndSQUpdateOptions
    {
        public DateTime? ReleaseDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int[] EvaluatorIds { get; set; }

        public string Action { get; set; }

        public int[] TQIds { get; set; }
        
        public int[] SQIds { get; set; }

        public int CheckStarted { get; set; }

        public bool RemoveSignOffs { get; set; }
    }
}
