namespace QTD2.Domain.Entities.Core
{
    public class ToolGroup_Tool : Common.Entity
    {
        public int ToolId { get; set; }

        public int ToolGroupId { get; set; }

        public virtual Tool Tool { get; set; }

        public virtual ToolGroup ToolGroup { get; set; }

        public ToolGroup_Tool(int toolId, int toolGroupId)
        {
            ToolId = toolId;
            ToolGroupId = toolGroupId;
        }

        public ToolGroup_Tool()
        {
        }
    }
}
