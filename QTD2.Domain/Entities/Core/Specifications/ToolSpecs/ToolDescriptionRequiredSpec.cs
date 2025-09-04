namespace QTD2.Domain.Entities.Core.Specifications.ToolSpecs
{
    public class ToolDescriptionRequiredSpec : Interfaces.Specification.ISpecification<Tool>
    {
        public bool IsSatisfiedBy(Tool entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
