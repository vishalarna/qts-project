using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Tool : Entity
    {
        public int EOId { get; set; }

        public int ToolId { get; set; }

        public virtual Tool Tool { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public EnablingObjective_Tool(EnablingObjective enablingObjective, Tool tool)
        {
            EOId = enablingObjective.Id;
            ToolId = tool.Id;
            EnablingObjective = enablingObjective;
            Tool = tool;
        }

        public EnablingObjective_Tool()
        {
        }

        public Version_EnablingObjective_Tool_Link CreateSnapshot()
        {
            return new Version_EnablingObjective_Tool_Link(this.EOId, this.ToolId);
        }
    }
}
