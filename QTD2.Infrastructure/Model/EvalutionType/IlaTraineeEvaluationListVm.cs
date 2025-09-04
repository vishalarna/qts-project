using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EvalutionType
{
    public class IlaTraineeEvaluationListVm
    {
        public string EvaluationMethodType { get; set; }
        public string EvaluationType { get; set; }
        public string EvaluationDescription { get; set; }
        public int EvaluationId { get; set; }
        public bool IsActive { get; set; }
        public object data { get; set; }

    }
}
