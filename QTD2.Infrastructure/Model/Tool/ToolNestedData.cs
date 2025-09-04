using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Tool
{
    public class ToolNestedData
    {
        public ToolCategoryData Category { get; set; }
        public List<ToolOptions> Tools { get; set; }
    }

    public class ToolDashboardModel
    {
        public int ActiveCaategories { get; set; }
        public int InActiveCaategories { get; set; }
        public int ActiveToolsCount { get; set; }
        public int InActiveToolsCount { get; set; }
        public int ToolsNotLinkedToTaskCount { get; set; }
        public int ToolsNotLinkedToSkillAssesmentCount { get; set; }
    }
}
