using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ToolCategory_StatusHistory : Common.Entity
    {

        public int ToolCategoryId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? ChangeEffectiveDate { get; set; }

        public virtual ToolCategory ToolCategory { get; set; }

        public ToolCategory_StatusHistory(int toolCategoryId, string changeNotes, DateTime changeEffectiveDate)
        {
            ToolCategoryId = toolCategoryId;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
        }

        public ToolCategory_StatusHistory()
        {
        }
    }
}
