using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class ToolGroup : Common.Entity
    {
        public string Description { get; set; }

        public virtual ICollection<ToolGroup_Tool> ToolGroup_Tools { get; set; } = new List<ToolGroup_Tool>();

        public ToolGroup(string description, bool active)
        {
            Description = description;
            Active = active;
        }

        public ToolGroup()
        {
        }

        public ToolGroup_Tool AddTool(Tool tool)
        {
            ToolGroup_Tool toolGroup_Tool = ToolGroup_Tools.FirstOrDefault(x => x.ToolId == tool.Id);
            if (toolGroup_Tool == null)
            {
                toolGroup_Tool = new ToolGroup_Tool(tool.Id, Id);
                AddEntityToNavigationProperty<ToolGroup_Tool>(toolGroup_Tool);
            }

            return toolGroup_Tool;
        }

        public void RemoveTool(Tool tool)
        {
            ToolGroup_Tool toolGroup_Tool = ToolGroup_Tools.FirstOrDefault(x => x.ToolId == tool.Id);
            if (toolGroup_Tool != null)
            {
                RemoveEntityFromNavigationProperty<ToolGroup_Tool>(toolGroup_Tool);
            }
        }
    }
}
