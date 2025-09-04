using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQwithTask_EvalDetailVM
    {
        public string TaskFullNumber { get; set; }
        public string Description { get; set; }
        public List<string> EvaluatorsName { get; set; } = new List<string>();

        public TQwithTask_EvalDetailVM(){}

        public TQwithTask_EvalDetailVM(string taskFullNumber,string description, List<string> evaluatorsName)
        {
            TaskFullNumber = taskFullNumber;
            EvaluatorsName = evaluatorsName ?? new List<string>();
            Description = description;
        }
    }

}
