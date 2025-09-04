using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Tool : Common.Entity
    {
        public int ToolId { get; set; }

        public string Description { get; set; }

        public int MinorVersion { get; set; }

        public int MajorVersion { get; set; }

        public virtual Tool Tool { get; set; }

        public virtual ICollection<Version_Procedure_Tool_Link> Version_Procedure_Tool_Links { get; set; } = new List<Version_Procedure_Tool_Link>();

        public virtual ICollection<Version_Task_Tool_Link> Version_Task_Tool_Links { get; set; } = new List<Version_Task_Tool_Link>();

        public virtual ICollection<Version_EnablingObjective_Tool_Link> Version_EnablingObjective_Tool_Links { get; set; } = new List<Version_EnablingObjective_Tool_Link>();

        public Version_Tool()
        {
        }

        public Version_Tool(Tool tool)
        {
            ToolId = tool.Id;
            Description = tool.Name;
        }
    }
}
