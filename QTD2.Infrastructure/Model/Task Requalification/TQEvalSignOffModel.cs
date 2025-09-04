using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQEvalSignOffModel
    {
        public string EvaluatorName { get; set; }

        public DateTime? SignOffDate { get; set; }

        public int EvaluatorId { get; set; }

        public TQEvalSignOffModel()
        {

        }

        public TQEvalSignOffModel(int evalId,string evalName,DateTime? signOffDate)
        {
            EvaluatorId = evalId;
            EvaluatorName = evalName;
            SignOffDate = signOffDate;
        }
    }
}
