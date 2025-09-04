using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Tool_StatusHistory
{
    public class Tool_StatusHistoryUpdateOptions
    {
        public int ToolId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }
    }
}
