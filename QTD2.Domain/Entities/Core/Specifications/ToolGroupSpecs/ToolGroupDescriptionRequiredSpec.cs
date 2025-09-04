namespace QTD2.Domain.Entities.Core.Specifications.ToolGroupSpecs
{
    public class ToolGroupDescriptionRequiredSpec : Interfaces.Specification.ISpecification<ToolGroup>
    {
        public bool IsSatisfiedBy(ToolGroup entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
