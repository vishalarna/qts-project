using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_ImplementSpecs
{
    public class InstructorWorkbook_ILA_ImplementImplementResultRequiredSpecs : ISpecification<InstructorWorkbook_ILA_Implement>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILA_Implement entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ImplementResult);
        }
    }
}
