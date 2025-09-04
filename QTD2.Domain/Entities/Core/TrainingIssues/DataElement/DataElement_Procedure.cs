using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_Procedure : TrainingIssue_DataElement 
    {
        public int? ProcedureId { get; set; }
        public Procedure Procedure { get; set; }

        public DataElement_Procedure(int trainingIssueId, int? procedureId) : base(trainingIssueId)
        {
            ProcedureId = procedureId;
        }

        public DataElement_Procedure()
        {

        }
    }
}
