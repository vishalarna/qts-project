using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAEvalMethodVM
    {
        public string EvaluationMethod { get; set; }

        public bool UseForEMP { get; set; }
        public bool IsPubliclyAvailableILA {  get; set; }
    }
}
