using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_Tool : TrainingIssue_DataElement
    {
        public int? ToolId { get; set; }
        public Tool Tool { get; set; }
        public DataElement_Tool(int trainingIssueId, int? toolId) : base(trainingIssueId)
        {
            ToolId = toolId;
        }
        public DataElement_Tool()
        {

        }
    }
}
