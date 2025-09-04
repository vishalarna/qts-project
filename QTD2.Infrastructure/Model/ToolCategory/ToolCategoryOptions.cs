using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ToolCategory
{
    public class ToolCategoryOptions
    {
        public string ActionType { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }

    public class ToolsBulkEditOptions
    {
        public string ActionType { get; set; }
        public List<int> toolIds { get; set; }
        public List<int>? LinkedIds { get; set; }
    }


    public class LinkToolsOptions
    {
        public List<int> toolIds { get; set; }
        public List<int> LinkedIds { get; set; }
    }
}
