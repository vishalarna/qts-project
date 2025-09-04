using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_Tool_Link : Common.Entity
    {
        public int SafetyHazardId { get; set; }

        public int ToolId { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public virtual Tool Tool { get; set; }

        public SafetyHazard_Tool_Link(SaftyHazard saftyHazard, Tool tool)
        {
            SaftyHazard = saftyHazard;
            Tool = tool;
            SafetyHazardId = saftyHazard.Id;
            ToolId = tool.Id;
        }

        public SafetyHazard_Tool_Link()
        {
        }
    }
}
