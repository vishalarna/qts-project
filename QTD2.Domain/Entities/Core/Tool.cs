using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Tool : Common.Entity
    {
        public int ToolCategoryId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string Hyperlink { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string Description { get; set; }

        public byte[] Upload { get; set; }

        public virtual ICollection<ToolGroup_Tool> ToolGroup_Tools { get; set; } = new List<ToolGroup_Tool>();

        public virtual ICollection<Task_Tool> Task_Tools { get; set; } = new List<Task_Tool>();

        public virtual ICollection<Version_Tool> Version_Tools { get; set; } = new List<Version_Tool>();

        public virtual ToolCategory ToolCategory { get; set; }

        public virtual ICollection<SafetyHazard_Tool_Link> SafetyHazard_Tool_Links { get; set; } = new List<SafetyHazard_Tool_Link>();

        public virtual ICollection<Tool_StatusHistory> Tool_StatusHistories { get; set; } = new List<Tool_StatusHistory>();

        public virtual ICollection<EnablingObjective_Tool> EnablingObjective_Tools { get; set; } = new List<EnablingObjective_Tool>();

        public Tool(int toolCategoryId, string number, string name, string hyperlink, DateTime? effectiveDate, byte[] upload, string description)
        {
            ToolCategoryId = toolCategoryId;
            Number = number;
            Name = name;
            Hyperlink = hyperlink;
            EffectiveDate = effectiveDate;
            Upload = upload;
            Description = description;
        }

        public Tool()
        {
        }

        public Version_Tool CreateSnapshot()
        {
            return new Version_Tool(this);
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnTool_Deleted(this));
        }
    }
}
