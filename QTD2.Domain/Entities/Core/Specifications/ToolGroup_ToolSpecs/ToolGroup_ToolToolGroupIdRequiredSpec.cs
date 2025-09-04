namespace QTD2.Domain.Entities.Core.Specifications.ToolGroup_ToolSpecs
{
    public class ToolGroup_ToolToolGroupIdRequiredSpec : Interfaces.Specification.ISpecification<ToolGroup_Tool>
    {
        public bool IsSatisfiedBy(ToolGroup_Tool entity, params object[] args)
        {
            return entity.ToolGroupId > 0;
        }
    }
}
