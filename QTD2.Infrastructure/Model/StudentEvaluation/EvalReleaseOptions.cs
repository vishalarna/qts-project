using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluation
{
    public class EvalReleaseOptions
    {
        public int ClassId { get; set; }

        public int EvalId { get; set; }

        public int EmpId { get; set; }

        public string Action { get; set; }
    }
}
