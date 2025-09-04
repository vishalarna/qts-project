using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Tool_StatusHistory : Common.Entity
    {
        public int ToolId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? ChangeEffectiveDate { get; set; }

        public virtual Tool Tool { get; set; }

        public Tool_StatusHistory(int toolId, string changeNotes, DateTime changeEffectiveDate)
        {
            ToolId = toolId;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
        }

        public Tool_StatusHistory()
        {
        }
    }
}
